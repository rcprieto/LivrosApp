using API.Data;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;

[AllowAnonymous]
public class AccountController : BaseApiController
{
	private readonly UserManager<AppUser> _userMananger;
	private readonly DataContext _context;
	private readonly ITokenService _tokenService;
	private readonly IMapper _mapper;

	public AccountController(UserManager<AppUser> userMananger, DataContext context, ITokenService tokenService, IMapper mapper)
	{
		_userMananger = userMananger;
		_context = context;
		_tokenService = tokenService;
		_mapper = mapper;
	}




	[HttpPost("login")]
	public async Task<ActionResult<UserDto>> Login(LoginDto model)
	{
		var user = await _userMananger.Users
			.SingleOrDefaultAsync(m => m.Email == model.Email);

		if (user == null) return Unauthorized();

		var result = await _userMananger.CheckPasswordAsync(user, model.Password);

		if (!result) return Unauthorized();

		return new UserDto
		{
			UserName = user.UserName,
			Token = await _tokenService.CreateToken(user),
			Email = user.Email,
			Id = user.Id
		};

	}


	private async Task<bool> UserExists(string username)
	{
		return await _userMananger.Users.AnyAsync(c => c.UserName.ToUpper().Equals(username.ToUpper().Trim()));
	}


}
