using Ragkivio.Graphql.Configuration.Presentation;
using Ragkivio.Graphql.Configuration.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOptionPattern()
    .AddPresentation();

var app = builder.Build();

app.MapGraphQL();
app.RunWithGraphQLCommands(args);