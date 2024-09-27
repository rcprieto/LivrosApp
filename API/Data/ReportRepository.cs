using System;
using API.DTOs;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class ReportRepository : IReportRepository
{
	private readonly DataContext _context;

	public ReportRepository(DataContext context)
	{
		_context = context;
	}

	public async Task<ReportsDto> ReportDash(FiltersParams filters)
	{
		var livros = await _context.Livros.Where(c => c.AppUserId == filters.UserId).ToListAsync();
		ReportsDto retorno = new ReportsDto();

		retorno.TotalLivros = livros.Count();
		var livrsoAno = livros.Where(c => c.FimLeitura.HasValue && c.FimLeitura.Value.Year == filters.Ano);
		retorno.TotalLivrosAno = livrsoAno.Count();
		retorno.TotalPaginas = livros.Sum(c => c.Paginas ?? 0);
		retorno.TotalPaginasAno = livrsoAno.Sum(c => c.Paginas ?? 0);

		retorno.ReportListaLivros.AddRange(
			livros
			.Where(c => c.FimLeitura.HasValue)
			.OrderByDescending(c => c.FimLeitura)
			.Take(10)
			.Select(c => new ReportListaLivros
			{
				Nome = c.Nome,
				Genero = c.Categoria,
				Data = c.FimLeitura!.Value.ToString("dd/MM/yyyy"),
				Paginas = c.Paginas,
				Autor = c.Autor
			}));

		for (int mes = 0; mes < 12; mes++)
		{
			retorno.PaginasPorMes[mes] = livrsoAno
			.Where(c => c.FimLeitura.HasValue && c.FimLeitura.Value.Month == mes + 1)
			.Sum(c => c.Paginas ?? 0);

		}


		return retorno;



	}
}
