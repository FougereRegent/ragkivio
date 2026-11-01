using Ragkivio.Domain.Common;

namespace Ragkivio.Domain.Project;

public interface IProjectRepository : IGenericRepository<Project> {
    Task<Project?> GetByNameAsync(string name, CancellationToken token);
}