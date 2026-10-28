using Ragkivio.Application.Configuration;
using Ragkivio.Domain.Project;
using Ragkivio.Persistence.Common;

namespace Ragkivio.Persistence.Project;

using ProjectPersistence = Ragkivio.Persistence.Project.Project;
using ProjectDomain = Ragkivio.Domain.Project.Project;

internal sealed class ProjectRepository : GenericRepository<ProjectPersistence, ProjectDomain>, IProjectRepository
{
    public ProjectRepository(RagkivioContext context, IDomainEventCollector domainEventCollector)
        : base(context, domainEventCollector)
    {

    }

    private protected override void HydratePersistenceEntity(ProjectDomain domainEntity, ProjectPersistence persistenceEntity)
    {
        ProjectMapper.Hydrate(domainEntity, persistenceEntity);
    }

    private protected override IQueryable<ProjectDomain> ProjectionToDomain(IQueryable<ProjectPersistence> collection)
    {
        return collection.ProjectToDomain();
    }

    private protected override ProjectDomain ToDomain(ProjectPersistence entity)
    {
        return ProjectMapper.ToDomain(entity);
    }

    private protected override ProjectPersistence ToPersistence(ProjectDomain entity)
    {
        return ProjectMapper.ToPersistence(entity);
    }
}