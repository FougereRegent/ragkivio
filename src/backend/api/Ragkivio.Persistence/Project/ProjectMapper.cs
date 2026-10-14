namespace Ragkivio.Persistence.Project;

using Ragkivio.Domain.Common;
using Ragkivio.Domain.Project;
using Riok.Mapperly.Abstractions;
using ProjectDomain = Ragkivio.Domain.Project.Project;
using ProjectPersistence = Ragkivio.Persistence.Project.Project;

[Mapper]
internal static partial class ProjectMapper
{
    [MapProperty(source: nameof(ProjectPersistence.Color), target: [nameof(ProjectDomain.Color), nameof(ColorElement.ColorHex)])]
    [MapProperty(source: [nameof(ProjectDomain.Color), nameof(ColorElement.ColorHex)], target: nameof(ProjectDomain.Visibility))]
    public static partial ProjectDomain ToDomain(ProjectPersistence project);

    [MapProperty(target: nameof(ProjectPersistence.IsShared), source: nameof(ProjectDomain.Visibility), Use = nameof(MapToShared))]
    [MapProperty(source: [nameof(ProjectDomain.Color), nameof(ColorElement.ColorHex)], target: nameof(ProjectPersistence.Color))]
    public static partial ProjectPersistence ToPersistence(ProjectDomain project);

    public static partial IQueryable<ProjectDomain> ProjectToDomain(this IQueryable<ProjectPersistence> projects);


    public static partial void Hydrate(ProjectDomain source, ProjectPersistence target);

    private static ColorElement MapColor(string color)
    {
        return new ColorElement
        {
            ColorHex = color
        };
    }

    private static Visibility MapVisibility(bool isShared) {
        return isShared ? Visibility.SHARED : Visibility.PRIVATE;
    }

    private static bool MapToShared(Visibility visibility) {
        return visibility == Visibility.SHARED ? true : false;
    }

}