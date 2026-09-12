using Microsoft.EntityFrameworkCore;
using Ragkivio.Graphql.Configuration;
using Ragkivio.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConfig();

var app = builder.Build();

if (ShouldRunMigration(app.Environment))
{
    var cancellationTokenSource = new CancellationTokenSource();
    cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(30));
    await RunMigrationAsync(cancellationTokenSource.Token);
}

app.UseRouting();
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