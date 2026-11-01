namespace Ragkivio.Application.Project.Services;

using Ragkivio.Domain.Project;

internal sealed class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    public IQueryable<Domain.Project.Project> GetAll() 
        => projectRepository.GetAll();


    public async Task<bool> ProjectAlreadyExistAsync(string name, CancellationToken token) {
        var project = await projectRepository.GetByNameAsync(name, token);
        return project != null;
    }

    public async Task<bool> ProjectAlreadyExistAsync(Guid id, CancellationToken token)
    {
        var project = await projectRepository.GetByIdAsync(id, token);
        return project != null;
    }
}