using API.DTOs;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Policy = "UserRole")]
    public class ReportController : BaseApiController
    {
        private readonly IUnitOfWork _uow;

        public ReportController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet("dash")]
        public async Task<ActionResult<ReportsDto>> ReportDash([FromQuery] FiltersParams filters)
        {
            string userId = User.GetUserId();
            filters.UserId = userId;
            var retorno = await _uow.ReportRepository.ReportDash(filters);
            if (retorno != null)
            {
                return Ok(retorno);
            }
            else
            {
                return BadRequest("Sem livros");
            }
        }



    }
}
