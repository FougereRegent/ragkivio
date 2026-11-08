using Microsoft.EntityFrameworkCore;
using Ragkivio.Graphql.Configuration;
using Ragkivio.Infrastructure.Common;
using Ragkivio.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConfig();

var app = builder.Build();

using var migrationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(5));
using var initTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(5));

if (ShouldRunMigration(app.Environment))
{
    await RunMigrationAsync(migrationTokenSource.Token);
}

await RunInitAsync(initTokenSource.Token);

app.UseRouting();
app.UseCors("AllowAllOrigin");
app.UseAuthentication();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.RunWithGraphQLCommands(args);


async Task RunMigrationAsync(CancellationToken cancellationToken = default)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<RagkivioContext>();
    await dbContext.Database.MigrateAsync(cancellationToken);
}

bool ShouldRunMigration(IWebHostEnvironment env)
{
    return env.IsDevelopment();
}

async Task RunInitAsync(CancellationToken cancellationToken) 
{
    using var scope = app.Services.CreateScope();
    var initServices = scope.ServiceProvider.GetRequiredKeyedService<IInit>(InitName.InitStorage);

    await initServices.InitAsync(cancellationToken);
}