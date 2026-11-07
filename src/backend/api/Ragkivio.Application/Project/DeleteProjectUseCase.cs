namespace Ragkivio.Application.Project;

using System.Threading;
using Ragkivio.Application.Common;
using Ragkivio.Application.Common.Errors;
using Ragkivio.Application.Project.Dto;
using Ragkivio.Domain.Project;

public sealed class DeleteProjectUseCase(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork
        ) : IUseCaseAsync<Project, DeleteProjectRequest>
{
    private const string ressource = "project.delete";

    public async Task<FluentResults.Result<Project>> HandleAsync(DeleteProjectRequest input, CancellationToken token = default)
    {
        var project = await projectRepository.GetByIdAsync(input.Id, token);
        if (project == null)
        {
            return FluentResults.Result.Fail("");
        }
        try

        {
            await unitOfWork.ExecuteAsync(async () =>
            {
                await projectRepository.DeleteAsync(project, token: token);
            });
        }
        catch(UnitOfWorkSaveException ex)
        {
            return FluentResults.Result.Fail(new InfrastructureError(ressource, ex));
        }
        return project;
    }
}