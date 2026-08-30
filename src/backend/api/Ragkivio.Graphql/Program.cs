var builder = WebApplication.CreateBuilder(args);

builder.AddGraphQL();

var app = builder.Build();

app.Configuration.GetConnectionString
app.MapGraphQL();
app.RunWithGraphQLCommands(args);