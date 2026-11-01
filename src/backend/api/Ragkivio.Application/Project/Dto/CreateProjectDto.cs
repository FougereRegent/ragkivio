namespace Ragkivio.Application.Project.Dto;

public record CreateProjectRequest(
        string Name,
        string? Description,
        string Color
);