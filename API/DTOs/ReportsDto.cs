using System;

namespace API.DTOs;

public class ReportsDto
{
	public int TotalLivros { get; set; } = 0;
	public int TotalLivrosAno { get; set; } = 0;
	public int TotalPaginas { get; set; } = 0;
	public int TotalPaginasAno { get; set; } = 0;
	public List<ReportListaLivros> ReportListaLivros { get; set; } = new List<ReportListaLivros>();

	public int[] PaginasPorMes { get; set; } = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

}

public class ReportListaLivros
{
	public string? Nome { get; set; }
	public string? Data { get; set; }
	public int? Paginas { get; set; }
	public string? Genero { get; set; }
	public string? Autor { get; set; }
}


