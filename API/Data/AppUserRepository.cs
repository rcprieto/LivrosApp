using API.Data;
using API.Entities;
using API.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API;

public class AppUserRepository : IAppUserRepository
{
	private readonly DataContext _context;

	public AppUserRepository(DataContext context)
	{
		_context = context;
	}

	public async Task<AppUser?> GetUserById(string id)
	{
		return await _context.Users.FindAsync(id);
	}

	public async Task<AppUser?> GetUserByUsernameAsync(string username)
	{
		return await _context.Users
		.SingleOrDefaultAsync(c => c.UserName == username);
	}

	public async Task<IEnumerable<AppUser>> GetUsersAsync()
	{
		return await _context.Users
		.ToListAsync();
	}

	public async Task<PagedList<UserDto>> GetUsersPaginatedAsync(PaginationParams userParams)
	{
		var query = _context.Users
		.Select(c => new UserDto
		{
			Id = c.Id,
			Email = c.Email,
			UserName = c.UserName,
			Roles = c.UserRoles.Select(r => r.Role.Name).ToList()

		}).AsQueryable();

		//query = query.Where(c => c.UserName != userParams.CurrentUserName);



		return await PagedList<UserDto>
		.CreateAsync(query.AsNoTracking(), userParams.PageNumber, userParams.PageSize);

	}



	public void Update(AppUser user)
	{
		_context.Entry(user).State = EntityState.Modified;

	}
}
