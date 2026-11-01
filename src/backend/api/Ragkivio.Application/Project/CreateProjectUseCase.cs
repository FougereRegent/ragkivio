namespace Ragkivio.Application.Project;

using FluentResults;
using Ragkivio.Application.Common;
using Ragkivio.Application.Project.Dto;
using Ragkivio.Application.Project.Services;
using Ragkivio.Domain.Project;

public sealed class CreateProjectUseCase(
    IProjectService projectService,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork
        ) : IUseCaseAsync<Project, CreateProjectRequest>
{
    public async Task<Result<Project>> HandleAsync(CreateProjectRequest input, CancellationToken token = default)
    {
        var isExist = await projectService.ProjectAlreadyExistAsync(input.Name, token);
        if (isExist)
        {
            return Result.Fail("");
        }

        var project = new Project 
        {
            Name = input.Name,
            Color = new Domain.Common.ColorElement {
                ColorHex = input.Color
            }, 
            Description = input.Description,
            Visibility = input.Visibility
        };

        try
        {
            await unitOfWork.ExecuteAsync(async () =>
            {
                project = await projectRepository.SaveAsync(project, token);
            }, cancellationToken: token);
        }
        catch
        {
            Result.Fail("");
        }

        if(project is null) {
            return Result.Fail("");
        }

        return Result.Ok(project);
    }
}