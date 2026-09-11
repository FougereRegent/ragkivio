using System.Text.Json;

namespace Ragkivio.Persistence.Entities;

internal class User : Entity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? AuthId { get; set; } = null;
    public string? PhoneNumber { get; set; } = string.Empty;

    public JsonElement? Config { get; set; } = null!;
}