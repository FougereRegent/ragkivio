namespace Ragkivio.Application.Project;

using System.Threading;
using Ragkivio.Application.Common;
using Ragkivio.Application.Project.Dto;
using Ragkivio.Domain.Project;

public sealed class UpdateProjectUseCase(
        IUnitOfWork unitOfWork,
        IProjectRepository projectRepository
        ) : IUseCaseAsync<Project, UpdateProjectRequest>
{
    public async Task<FluentResults.Result<Project>> HandleAsync(UpdateProjectRequest input, CancellationToken token = default)
    {
        var project = await projectRepository.GetByIdAsync(input.Id, token);
        if (project == null)
        {
            return FluentResults.Result.Fail("");
        }

        try
        {
            project.Update(name: input.Name, description: input.Description, color: input.Color, visibility: input.Visibility);
            await unitOfWork.ExecuteAsync(async () =>
            {
                await projectRepository.UpdateAsync(project, token);
            });
        }
        catch
        {
            return FluentResults.Result.Fail("");
        }

        return FluentResults.Result.Ok(project);
    }
}