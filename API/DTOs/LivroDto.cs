using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class LivroDto
{
	public int Id { get; set; } = 0;

	[StringLength(150)]
	public required string Nome { get; set; }

	[StringLength(150)]
	public required string Autor { get; set; }
	public DateTime? FimLeitura { get; set; }

	public DateTime? InicioLeitura { get; set; }

	public int? Paginas { get; set; }

	public string? Categoria { get; set; }

	[StringLength(300)]
	public string? UrlCapa { get; set; }

	[StringLength(500)]
	public string? Resumo { get; set; }
	public string? AppUserId { get; set; }

}
