using API.Data;
using API.Entities;
using API.Helpers;
using API.Interfaces;
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
	private readonly IEmailService _emailService;

	public AccountController(UserManager<AppUser> userMananger, DataContext context, ITokenService tokenService, IMapper mapper, IEmailService emailService)
	{
		_userMananger = userMananger;
		_context = context;
		_tokenService = tokenService;
		_mapper = mapper;
		_emailService = emailService;
	}


	[HttpPost("login")]
	[AllowAnonymous]
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

	[HttpPost("reset")]
	[AllowAnonymous]
	public async Task<ActionResult<UserDto>> ResetPassword(ResetDto model)
	{
		try
		{

			var user = await _userMananger.Users.SingleOrDefaultAsync(m => m.Email == model.Email);

			if (user == null) return Ok();

			string novaSenha = Geral.GerarSenhaRandomicaLonga();

			var resetToken = await _userMananger.GeneratePasswordResetTokenAsync(user);
			var passwordChangeResult = await _userMananger.ResetPasswordAsync(user, resetToken, novaSenha);
			if (passwordChangeResult.Succeeded)
			{
				var retorno = _emailService.ResetarSenhaEmail(user.Email, novaSenha, novaSenha);
				return Ok();
			}
			else
				return Ok();

		}
		catch (Exception err)
		{
			return BadRequest(err.Message);
		}

	}


	private async Task<bool> UserExists(string username)
	{
		return await _userMananger.Users.AnyAsync(c => c.UserName.ToUpper().Equals(username.ToUpper().Trim()));
	}


}
