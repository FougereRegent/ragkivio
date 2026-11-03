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
        return FluentResults.Result.Ok();
    }
}