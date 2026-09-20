namespace Ragkivio.Application.User.Dto;

public sealed record UserInformationResponse
{
    public required Guid Id { get; init; }
    public required string Email { get; init; } = string.Empty;
    public required string FirstName { get; init; } = string.Empty;
    public required string LastName { get; init; } = string.Empty;
}
