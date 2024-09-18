using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[Authorize(Policy = "RequireAdminRole")]
public class UsersController : BaseApiController
{
	private readonly UserManager<AppUser> _userMananger;
	private readonly IUnitOfWork _uow;
	private readonly IMapper _mapper;
	private readonly ITokenService _tokenService;

	public UsersController(UserManager<AppUser> userMananger, IUnitOfWork uow, IMapper mapper, ITokenService tokenService)
	{
		_userMananger = userMananger;
		_uow = uow;
		_mapper = mapper;
		_tokenService = tokenService;
	}


	[HttpGet("{id}")]
	public async Task<ActionResult<AppUser>> GetUser(string id)
	{
		return await _uow.AppUserRepository.GetUserById(id);
	}

	[HttpGet]
	public async Task<ActionResult<PagedList<UserDto>>> GetUsers([FromQuery] PaginationParams paginationParams)
	{
		var users = await _uow.AppUserRepository.GetUsersPaginatedAsync(paginationParams);
		Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));
		if (users != null)
		{
			return Ok(users);
		}
		else
		{
			return BadRequest("Sem Usuários");
		}
	}

	[HttpPost("register")] //POST: api/user/register
	public async Task<ActionResult<UserDto>> Register(RegisterDto model)
	{
		var user = _mapper.Map<AppUser>(model);

		user.UserName = model.UserName.Trim();

		var result = await _userMananger.CreateAsync(user, model.Password);

		if (!result.Succeeded) return BadRequest(String.Join(", ", result.Errors.Select(c => c.Description)));

		var rolesResult = await _userMananger.AddToRoleAsync(user, "User");

		if (!rolesResult.Succeeded) return BadRequest(String.Join(", ", result.Errors.Select(c => c.Description)));

		return new UserDto
		{
			UserName = user.UserName,
			Token = await _tokenService.CreateToken(user),

		};

	}

	[HttpPut]
	public async Task<ActionResult> UpdateUser(UserDto model)
	{
		var user = await _uow.AppUserRepository.GetUserById(model.Id ?? "");
		if (user == null) return NotFound();

		user.UserName = model.UserName;
		//_mapper.Map(model, user);

		var teste = await _uow.Complete();

		if (teste) return NoContent();

		return BadRequest("Erro ao atualizar o usuário");


	}




}
