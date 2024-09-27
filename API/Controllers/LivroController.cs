using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Policy = "UserRole")]
    public class LivroController : BaseApiController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public LivroController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet("get-livros")]
        public async Task<ActionResult<PagedList<LivroDto>>> GetTodosLivro([FromQuery] PaginationParams paginationParams)
        {
            var livros = await _uow.LivroRepository.GetLivrosAsync();
            if (livros != null)
            {
                var retorno = _mapper.Map<List<LivroDto>>(livros);
                return Ok(retorno);
            }
            else
            {
                return BadRequest("Sem livros");
            }
        }



        [HttpGet]
        public async Task<ActionResult<PagedList<LivroDto>>> GetLivro([FromQuery] PaginationParams paginationParams)
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
        public async Task<ActionResult<Livro>> AddLivro([FromBody] LivroDto model)
        {

            model.AppUserId = User.GetUserId();
            var livro = _mapper.Map<Livro>(model);

            _uow.LivroRepository.AddLivro(livro);

            if (!await _uow.Complete())
                return BadRequest("Erro ao salvar");

            model.Id = livro.Id;

            return Ok(model);

        }

        [HttpPut]
        public async Task<ActionResult<Livro>> UpdateLivro(LivroDto model)
        {
            var livro = _mapper.Map<Livro>(model);

            _uow.LivroRepository.UpdateLivro(livro);

            if (!await _uow.Complete())
                return BadRequest("Erro ao atualizar");

            return Ok(model);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLivro(int id)
        {


            var item = await _uow.LivroRepository.GetLivroByIdAsync(id);
            if (User.GetUserId() == item.AppUserId)
            {
                if (item != null)
                    _uow.LivroRepository.DeleteLivro(item);

                if (!await _uow.Complete())
                    return BadRequest("Erro ao excluir");

                return Ok();
            }
            else
            {
                return BadRequest("Livro de outro usuário");
            }


        }

    }
}
