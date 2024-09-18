using API.Entities;
using API.Helpers;

namespace API;

public interface IAppUserRepository
{
	void Update(AppUser user);
	Task<IEnumerable<AppUser>> GetUsersAsync();
	Task<AppUser?> GetUserById(string id);
	Task<AppUser?> GetUserByUsernameAsync(string username);
	Task<PagedList<UserDto>> GetUsersPaginatedAsync(PaginationParams userParams);

}
