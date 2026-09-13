namespace Ragkivio.Application.User.Dto;

public record RegisterUserDto(
        string Email,
        string FirstName,
        string LastName,
        string? PhoneNumber,
        );