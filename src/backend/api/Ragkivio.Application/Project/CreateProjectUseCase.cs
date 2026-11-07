namespace Ragkivio.Application.Project;

using FluentResults;
using Ragkivio.Application.Common;
using Ragkivio.Application.Common.Errors;
using Ragkivio.Application.Project.Dto;
using Ragkivio.Application.Project.Services;
using Ragkivio.Domain.Common.Exceptions;
using Ragkivio.Domain.Project;

public sealed class CreateProjectUseCase(
    IProjectService projectService,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork
        ) : IUseCaseAsync<Project, CreateProjectRequest>
{
    private const string resource = "project.create";

    public async Task<Result<Project>> HandleAsync(CreateProjectRequest input, CancellationToken token = default)
    {
        var isExist = await projectService.ProjectAlreadyExistAsync(input.Name, token);
        if (isExist)
        {
            return Result.Fail(new AlreadyExistError(resource, input.Name));
        }

        try
        {
            var project = new Project
            {
                Name = input.Name,
                Color = new Domain.Common.ColorElement
                {
                    ColorHex = input.Color
                },
                Description = input.Description,
                Visibility = input.Visibility
            };

            await unitOfWork.ExecuteAsync(async () =>
            {
                project = await projectRepository.SaveAsync(project, token);
            }, cancellationToken: token);

            return Result.Ok(project);
        }
        catch (BusinessException ex)
        {
            return Result.Fail(new DomainError(resource, ex));
        }
        catch (UnitOfWorkSaveException ex)
        {
            return Result.Fail(new InfrastructureError(resource, ex));
        }
    }
}
