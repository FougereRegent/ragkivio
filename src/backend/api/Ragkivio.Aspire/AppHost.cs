var builder = DistributedApplication.CreateBuilder(args);

var env = builder.Configuration.GetSection("environmentVariables");

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


var graphqlApi = builder.AddProject<Projects.Ragkivio_Graphql>("graphql-api")
                    .WithOtlpExporter()
                    .WaitFor(db)
                    .WaitFor(rabbitmq)
                    .WithReference(db)
                    .WithReference(rabbitmq)
                    .WithEnvironment("RAGKIVIO_AUTHORITY", env["RAGKIVIO_AUTHORITY"])
                    .WithEnvironment("RAGKIVIO_AUDIENCE", env["RAGKIVIO_AUDIENCE"])
                    .WithEnvironment("RAGKIVIO_AUTH_ID", env["RAGKIVIO_AUTH_CLIENT_ID"])
                    .WithEnvironment("RAGKIVIO_AUTH_DOMAIN", env["RAGKIVIO_AUTH_DOMAIN"])
                    .WithEnvironment("RAGKIVIO_AUTH_AUDIENCE", env["RAGKIVIO_AUTH_AUDIENCE"])
                    .WithEnvironment(context =>
                    {
                        context.EnvironmentVariables["RABBIT_MQ__USER"] = rabbitmq.Resource.UserNameParameter!;
                        context.EnvironmentVariables["RABBIT_MQ__PASSWORD"] = rabbitmq.Resource.PasswordParameter!;
                        context.EnvironmentVariables["RABBIT_MQ__HOST"] = rabbitmq.Resource.PrimaryEndpoint.Property(EndpointProperty.Host);
                        context.EnvironmentVariables["RABBIT_MQ__PORT"] = rabbitmq.Resource.PrimaryEndpoint.Property(EndpointProperty.Port);
                        context.EnvironmentVariables["ConnectionStrings__ragkivio"] = db.Resource.ConnectionStringExpression;
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