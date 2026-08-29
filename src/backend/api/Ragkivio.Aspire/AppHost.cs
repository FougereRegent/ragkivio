var builder = DistributedApplication.CreateBuilder(args);

var env = builder.Configuration.GetSection("environmentVariables");

var postgres = builder.AddPostgres("postgres")
                        .WithPgAdmin(configureContainer: opts =>
                        {
                            opts.WithEndpoint("http", endpoint => endpoint.Port = 8085);
                        })
                        .WithDataVolume("markivio-db");

var db = postgres.AddDatabase("markivio");

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
                    .WithEnvironment("MARKIVIO_AUTHORITY", env["MARKIVIO_AUTHORITY"])
                    .WithEnvironment("MARKIVIO_AUDIENCE", env["MARKIVIO_AUDIENCE"])
                    .WithEnvironment("MARKIVIO_AUTH_ID", env["MARKIVIO_AUTH_CLIENT_ID"])
                    .WithEnvironment("MARKIVIO_AUTH_DOMAIN", env["MARKIVIO_AUTH_DOMAIN"])
                    .WithEnvironment("MARKIVIO_AUTH_AUDIENCE", env["MARKIVIO_AUTH_AUDIENCE"])
                    .WithEnvironment(context =>
                    {
                        context.EnvironmentVariables["RABBIT_MQ__USER"] = rabbitmq.Resource.UserNameParameter!;
                        context.EnvironmentVariables["RABBIT_MQ__PASSWORD"] = rabbitmq.Resource.PasswordParameter!;
                        context.EnvironmentVariables["RABBIT_MQ__HOST"] = rabbitmq.Resource.PrimaryEndpoint.Property(EndpointProperty.Host);
                        context.EnvironmentVariables["RABBIT_MQ__PORT"] = rabbitmq.Resource.PrimaryEndpoint.Property(EndpointProperty.Port);
                    })
                    .WithUrl("/scalar")
                    .WithUrl("/graphql");

builder.Build().Run();