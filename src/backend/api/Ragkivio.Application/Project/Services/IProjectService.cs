namespace Ragkivio.Application.Project.Services;

using Ragkivio.Domain.Project;

public interface IProjectService 
{
    IQueryable<Project> GetAll();
    Task<bool> ProjectAlreadyExistAsync(string name, CancellationToken token);
    Task<bool> ProjectAlreadyExistAsync(Guid id, CancellationToken token);
}