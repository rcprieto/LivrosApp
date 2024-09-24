using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

public class Livro
{

	public int Id { get; set; }
	[StringLength(150)]
	public required string Nome { get; set; }

	[StringLength(150)]
	public required string Autor { get; set; }
	public DateTime? FimLeitura { get; set; }

	public DateTime? InicioLeitura { get; set; }

	[StringLength(300)]
	public string? UrlCapa { get; set; }

	[StringLength(500)]
	public string? Resumo { get; set; }

	public required string AppUserId { get; set; }

	[ForeignKey("AppUserId")]
	public AppUser? AppUser { get; set; }



}

public class LivroMock
{
	public int Id { get; set; }
	public string? Nome { get; set; }
	public string? Autor { get; set; }
	public string? Data { get; set; }
}
