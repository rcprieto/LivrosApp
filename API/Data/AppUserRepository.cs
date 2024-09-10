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



	public void Update(AppUser user)
	{
		_context.Entry(user).State = EntityState.Modified;

	}
}
