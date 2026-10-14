namespace Ragkivio.Persistence.Project;

using Ragkivio.Domain.Common;
using Ragkivio.Domain.Project;
using Riok.Mapperly.Abstractions;
using ProjectDomain = Ragkivio.Domain.Project.Project;
using ProjectPersistence = Ragkivio.Persistence.Project.Project;

[Mapper]
internal static partial class ProjectMapper
{
    [MapProperty(nameof(ProjectPersistence.Color), nameof(ProjectDomain.Color), Use = nameof(MapColor))]
    [MapProperty(nameof(ProjectPersistence.IsShared), nameof(ProjectDomain.Visibility), Use = nameof(MapVisibility))]
    public static partial ProjectDomain ToDomain(ProjectPersistence project);

    [MapProperty(nameof(ProjectDomain.Visibility), nameof(ProjectPersistence.IsShared), Use = nameof(MapToShared))]
    [MapProperty(nameof(ProjectDomain.Color), nameof(ProjectPersistence.Color), Use = nameof(MapColorHex))]
    public static partial ProjectPersistence ToPersistence(ProjectDomain project);

    public static partial IQueryable<ProjectDomain> ProjectToDomain(this IQueryable<ProjectPersistence> projects);


    [MapProperty(nameof(ProjectDomain.Visibility), nameof(ProjectPersistence.IsShared), Use = nameof(MapToShared))]
    [MapProperty(nameof(ProjectDomain.Color), nameof(ProjectPersistence.Color), Use = nameof(MapColorHex))]
    public static partial void Hydrate(ProjectDomain source, ProjectPersistence target);

    private static ColorElement MapColor(string color)
    {
        return new ColorElement
        {
            ColorHex = color
        };
    }

    private static string MapColorHex(ColorElement color)
    {
        return color.ColorHex;
    }

    private static Visibility MapVisibility(bool isShared) {
        return isShared ? Visibility.SHARED : Visibility.PRIVATE;
    }

    private static bool MapToShared(Visibility visibility) {
        return visibility == Visibility.SHARED ? true : false;
    }

}