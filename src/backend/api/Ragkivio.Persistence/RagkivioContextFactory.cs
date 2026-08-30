using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ragkivio.Persistence;

public class RagkivioContextFactory : IDesignTimeDbContextFactory<RagkivioContext>
{
    public RagkivioContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RagkivioContext>();
        optionsBuilder.UseNpgsql(GetConnectionString());
        return new RagkivioContext(optionsBuilder.Options);
    }

    private static string GetConnectionString()
    {
        return Environment.GetEnvironmentVariable("DB__CONNECTION_STRING")
            ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
            ?? ReadAppSettings("ConnectionStrings:DefaultConnection")
            ?? ReadAppSettings("DB:CONNECTION_STRING")
            ?? throw new InvalidOperationException(
                "Impossible de trouver une chaîne de connexion pour le design time. " +
                "Définissez 'DB__CONNECTION_STRING' ou 'ConnectionStrings__DefaultConnection' " +
                "dans l'environnement, ou renseignez 'ConnectionStrings:DefaultConnection' dans appsettings.json.");
    }

    private static string? ReadAppSettings(string key)
    {
        try
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            foreach (var fileName in new[] { "appsettings.json", $"appsettings.{environment}.json" })
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                if (!File.Exists(path))
                {
                    continue;
                }

                using var document = JsonDocument.Parse(File.ReadAllText(path));
                if (TryGetValue(document.RootElement, key, out var value))
                {
                    return value;
                }
            }
        }
        catch
        {
        }

        return null;
    }

    private static bool TryGetValue(JsonElement element, string key, out string? value)
    {
        value = null;
        foreach (var segment in key.Split(':'))
        {
            if (!element.TryGetProperty(segment, out element))
            {
                return false;
            }
        }

        if (element.ValueKind is JsonValueKind.String)
        {
            value = element.GetString();
            return true;
        }

        return false;
    }
}