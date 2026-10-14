using Ragkivio.Persistence.Common.Entities;

namespace Ragkivio.Persistence.Project;

internal sealed class Project : EntityWithTenancy
{
    public const int NameSizeMax = 64;
    public const int DescriptionMaxSize = 255;

    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = null!;
    public string Color { get; set; } = null!;
    public bool IsShared { get; set; } = false;
}