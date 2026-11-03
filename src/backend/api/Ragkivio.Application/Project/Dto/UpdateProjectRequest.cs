namespace Ragkivio.Application.Project.Dto;

public record UpdateProjectRequest(Guid id,
        string Name,
        string Description
);