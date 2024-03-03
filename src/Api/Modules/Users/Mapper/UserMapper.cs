using Riok.Mapperly.Abstractions;

namespace Api.Modules.Users;

[Mapper]
public static partial class UserMapperQuery
{
    public static partial IQueryable<UserListDto> MapUserToListDto(this IQueryable<AppUser> q);
}