using API.Entities;
using API.Helpers;

namespace API;

public interface IAppUserRepository
{
	void Update(AppUser user);
	Task<IEnumerable<AppUser>> GetUsersAsync();
	bool GetUserByEmail(string id, string email);
	Task<AppUser?> GetUserById(string id);
	Task<AppUser?> GetUserByUsernameAsync(string username);
	Task<PagedList<UserDto>> GetUsersPaginatedAsync(PaginationParams userParams);

}
