namespace Ragkivio.Application.Project;

using System.Threading;
using Ragkivio.Application.Common;
using Ragkivio.Application.Common.Errors;
using Ragkivio.Application.Project.Dto;
using Ragkivio.Domain.Common.Exceptions;
using Ragkivio.Domain.Project;

public sealed class UpdateProjectUseCase(
        IUnitOfWork unitOfWork,
        IProjectRepository projectRepository
        ) : IUseCaseAsync<Project, UpdateProjectRequest>
{
    private const string resource = "project.update";

    public async Task<FluentResults.Result<Project>> HandleAsync(UpdateProjectRequest input, CancellationToken token = default)
    {
        var project = await projectRepository.GetByIdAsync(input.Id, token);
        if (project == null)
        {
            return FluentResults.Result.Fail(new NotFoundError(resource, input.Id.ToString()));
        }

        try
        {
            project.Update(name: input.Name, description: input.Description, color: input.Color, visibility: input.Visibility);
            await unitOfWork.ExecuteAsync(async () =>
            {
                await projectRepository.UpdateAsync(project, token);
            });
        }
        catch (BusinessException ex)
        {
            return FluentResults.Result.Fail(new DomainError(resource, ex));
        }
        catch (UnitOfWorkSaveException ex)
        {
            return FluentResults.Result.Fail(new InfrastructureError(resource, ex));
        }

        return FluentResults.Result.Ok(project);
    }
}