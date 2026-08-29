var builder = WebApplication.CreateBuilder(args);

builder.AddGraphQL();

var app = builder.Build();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);