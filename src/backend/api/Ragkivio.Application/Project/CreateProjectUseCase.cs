namespace Ragkivio.Application.Project;

using Ragkivio.Application.Common;
using Ragkivio.Application.Project.Dto;
using Ragkivio.Domain.Project;

public sealed class CreateProjectUseCase : IUseCaseAsync<Project, CreateProjectRequest>
{
    public Task<FluentResults.Result<Project>> HandleAsync(CreateProjectRequest input, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
