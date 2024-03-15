using Riok.Mapperly.Abstractions;

namespace Common.Modules.User;

[Mapper]
public static partial class UserMapperQuery
{
    public static partial IQueryable<UserListDto> MapUserToListDto(this IQueryable<AppUser> q);
}