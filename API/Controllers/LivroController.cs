using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class LivroController : BaseApiController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public LivroController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<Livro>>> GetLivro([FromQuery] PaginationParams paginationParams)
        {
            var livros = await _uow.LivroRepository.GetLivroByUsernameAsync(paginationParams, User.GetUserId());
            if (livros != null)
            {
                Response.AddPaginationHeader(new PaginationHeader(livros.CurrentPage, livros.PageSize, livros.TotalCount, livros.TotalPages));
                return Ok(livros);
            }
            else
            {
                return BadRequest("Sem livros");
            }
        }

        [HttpPost("add-livro")]
        public async Task<ActionResult<Livro>> AddLivro(Livro model)
        {

            model.AppUserId = User.GetUserId();

            _uow.LivroRepository.AddLivro(model);
            if (await _uow.Complete())
                return BadRequest("Erro ao salvar");

            return Ok(model);

        }

        [HttpPut]
        public async Task<ActionResult<Livro>> UpdateLivro(Livro model)
        {
            _uow.LivroRepository.UpdateLivro(model);

            if (await _uow.Complete())
                return BadRequest("Erro ao atualizar");

            return Ok(model);

        }

        // [HttpPut]
        // public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        // {
        //     var user = await _appUserRepository.GetUserByUsernameAsync(User.GetUserName());
        //     if (user == null) return NotFound();

        //     _mapper.Map(memberUpdateDto, user);

        //     if (await _appUserRepository.SaveAllAsync()) return NoContent();

        //     return BadRequest("Erro ao atualizar o usuário");


        // }
    }
}
