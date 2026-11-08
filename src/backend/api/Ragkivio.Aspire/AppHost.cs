using System.Reflection;
using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

// Charge explicitement les appsettings depuis le dossier du projet AppHost,
// quel que soit le working directory (IDE, CLI, exécution directe du dll).
var appHostDirectory = Assembly.GetEntryAssembly()
    ?.GetCustomAttributes<AssemblyMetadataAttribute>()
    .FirstOrDefault(attribute => attribute.Key == "apphostprojectpath")?.Value
    ?? Directory.GetCurrentDirectory();

builder.Configuration
    .AddJsonFile(Path.Combine(appHostDirectory, "appsettings.json"), optional: true, reloadOnChange: true)
    .AddJsonFile(Path.Combine(appHostDirectory, $"appsettings.{builder.Environment.EnvironmentName}.json"), optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args);

var env = builder.Configuration;

foreach (var requiredKey in new[] { "AUTH:AUTHORITY", "AUTH:AUDIENCE", "AUTH:AUTH_ID", "MINIO:USER", "MINIO:PASSWORD" })
{
    if (string.IsNullOrWhiteSpace(env[requiredKey]))
    {
        Console.WriteLine(
            $"WARN: configuration '{requiredKey}' introuvable. " +
            $"Environment={builder.Environment.EnvironmentName}, dossier={appHostDirectory}");
    }
}
var postgres = builder.AddPostgres("postgres")
                        .WithImageTag("18")
                        .WithPgAdmin(configureContainer: opts =>
                        {
                            opts.WithEndpoint("http", endpoint => endpoint.Port = 8085);
                        })
                        .WithDataVolume("ragkivio-db");

var db = postgres.AddDatabase("ragkivio");

var username = builder.AddParameter("username", secret: true);
var password = builder.AddParameter("password", secret: true);

var rabbitmq = builder.AddRabbitMQ("broker", username, password)
                .WithManagementPlugin();

var minio = builder.AddContainer("minio", "chainguard/minio:latest")
                .WithArgs(
                        "server",
                        "/data",
                        "--console-address",
                        ":9001")
                .WithEnvironment("MINIO_ROOT_USER", env["MINIO:USER"])
                .WithEnvironment("MINIO_ROOT_PASSWORD", env["MINIO:PASSWORD"])
                .WithVolume("minio-data", "/data")
                .WithHttpEndpoint(port: 9000, targetPort: 9000, name: "s3")
                .WithHttpEndpoint(port: 9001, targetPort: 9001, name: "console")
                .WithHttpHealthCheck(path: "/minio/health/live", endpointName: "s3");


var graphqlApi = builder.AddProject<Projects.Ragkivio_Graphql>("graphql-api")
                    .WithOtlpExporter()
                    .WaitFor(db)
                    .WaitFor(rabbitmq)
                    .WaitFor(minio)
                    .WithReference(db)
                    .WithReference(rabbitmq)
                    .WithEnvironment("AUTH__AUTHORITY", env["AUTH:AUTHORITY"])
                    .WithEnvironment("AUTH__AUDIENCE", env["AUTH:AUDIENCE"])
                    .WithEnvironment("AUTH__AUTH_ID", env["AUTH:AUTH_ID"])
                    .WithEnvironment(context =>
                    {
                        context.EnvironmentVariables["RABBIT_MQ__USER"] = rabbitmq.Resource.UserNameParameter!;
                        context.EnvironmentVariables["RABBIT_MQ__PASSWORD"] = rabbitmq.Resource.PasswordParameter!;
                        context.EnvironmentVariables["RABBIT_MQ__HOST"] = rabbitmq.Resource.PrimaryEndpoint.Property(EndpointProperty.Host);
                        context.EnvironmentVariables["RABBIT_MQ__PORT"] = rabbitmq.Resource.PrimaryEndpoint.Property(EndpointProperty.Port);
                        context.EnvironmentVariables["ConnectionStrings__ragkivio"] = db.Resource.ConnectionStringExpression;
                        context.EnvironmentVariables["FILE_STORAGE__SERVER_URL"] = minio.GetEndpoint("s3");
                        context.EnvironmentVariables["FILE_STORAGE__ACCESS_KEY"] = env["MINIO:USER"];
                        context.EnvironmentVariables["FILE_STORAGE__SECRET_KEY"] = env["MINIO:PASSWORD"];
                        context.EnvironmentVariables["FILE_STORAGE__BUCKET_NAME"] = env["MINIO:BUCKET_NAME"] ?? "ragivio";
                    })
                    .WithUrl("/scalar")
                    .WithUrl("/graphql");

var frontend = builder.AddViteApp("frontend", "../../../frontend/ragivio-web")
                    .WithPnpm()
                    .WithRunScript("dev:custom")
                    .WithReference(graphqlApi)
                    .WithEndpoint("http", endpoint => endpoint.Port = 5173)
                    .WaitFor(graphqlApi)
                    .WithEnvironment("VITE_DEV", "true")
                    .WithEnvironment("VITE_APP_VERSION", "1.0.0");

builder.Build().Run();