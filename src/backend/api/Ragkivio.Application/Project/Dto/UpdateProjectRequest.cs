using Ragkivio.Domain.Project;

namespace Ragkivio.Application.Project.Dto;

public record UpdateProjectRequest(
        Guid Id,
        string Name,
        string Description,
        string? Color,
        Visibility Visibility
);