using Riok.Mapperly.Abstractions;
using System.Text.Json;

namespace Ragkivio.Persistence.User;

using ConfigDomain = Ragkivio.Domain.User.Config;
using UserDomain = Ragkivio.Domain.User.User;
using UserPersistence = Ragkivio.Persistence.User.User;

[Mapper]
internal static partial class UserMapper
{
    [MapperIgnoreSource(nameof(UserDomain.IsRegistered))]
    [MapperIgnoreTarget(nameof(UserPersistence.AuthId))]
    [MapperIgnoreTarget(nameof(UserPersistence.IsDelete))]
    [MapperIgnoreTarget(nameof(UserPersistence.DeletedAt))]
    public static partial UserPersistence ToPersistence(UserDomain user);

    [MapperIgnoreSource(nameof(UserPersistence.AuthId))]
    [MapperIgnoreSource(nameof(UserPersistence.IsDelete))]
    [MapperIgnoreSource(nameof(UserPersistence.DeletedAt))]
    [MapperIgnoreTarget(nameof(UserDomain.IsRegistered))]
    public static partial UserDomain ToDomain(UserPersistence user);

    public static partial IQueryable<UserDomain> ProjectToDomain(this IQueryable<UserPersistence> query);

    [UserMapping(Default = true)]
    [MapperIgnoreSource(nameof(UserPersistence.AuthId))]
    [MapperIgnoreSource(nameof(UserPersistence.IsDelete))]
    [MapperIgnoreSource(nameof(UserPersistence.DeletedAt))]
    [MapperIgnoreSource(nameof(UserPersistence.Config))]
    [MapperIgnoreTarget(nameof(UserDomain.IsRegistered))]
    [MapperIgnoreTarget(nameof(UserDomain.Config))]
    private static partial UserDomain MapToDomainForProjection(UserPersistence user);

    [UserMapping]
    private static JsonElement ConfigToJson(ConfigDomain config)
        => JsonSerializer.SerializeToElement(config);

    [UserMapping]
    private static ConfigDomain JsonToConfig(JsonElement json)
        => JsonSerializer.Deserialize<ConfigDomain>(json.GetRawText())!;
}