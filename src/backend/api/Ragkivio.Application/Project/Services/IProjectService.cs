namespace Ragkivio.Application.Project.Services;

public interface IProjectService 
{
    Task<bool> ProjectAlreadyExistAsync(string name, CancellationToken token);
    Task<bool> ProjectAlreadyExistAsync(Guid id, CancellationToken token);
}