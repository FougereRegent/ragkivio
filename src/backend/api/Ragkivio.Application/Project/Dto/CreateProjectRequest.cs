namespace Ragkivio.Application.Project.Dto;

using Ragkivio.Domain.Project;

public record CreateProjectRequest(
        string Name,
        string? Description,
        string Color,
        Visibility Visibility
);