
namespace Ragkivio.Application.Project.Services;


internal sealed class ProjectService : IProjectService
{
    public Task<bool> ProjectAlreadyExistAsync(string name, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ProjectAlreadyExistAsync(Guid id, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}