using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[Authorize]
public class UsersController : BaseApiController
{
	private readonly IUnitOfWork _uow;
	private readonly IMapper _mapper;

	public UsersController(IUnitOfWork uow, IMapper mapper)
	{
		_uow = uow;
		_mapper = mapper;
	}


	[HttpGet("{id}")]
	public async Task<ActionResult<AppUser>> GetUser(string id)
	{
		return await _uow.AppUserRepository.GetUserById(id);
	}

	[HttpPut]
	public async Task<ActionResult> UpdateUser(UserDto model)
	{
		var user = await _uow.AppUserRepository.GetUserByUsernameAsync(User.GetUserName());
		if (user == null) return NotFound();

		_mapper.Map(model, user);

		if (await _uow.Complete()) return NoContent();

		return BadRequest("Erro ao atualizar o usuário");


	}




}
