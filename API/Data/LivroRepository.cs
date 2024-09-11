using System;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Migrations;

public class LivroRepository : ILivroRepository
{
	private readonly DataContext _context;

	public LivroRepository(DataContext context)
	{
		_context = context;
	}

	public void AddLivro(Livro livro)
	{
		_context.Livros.Add(livro);
	}

	public async Task<Livro?> GetLivroById(int id)
	{
		return await _context.Livros.FindAsync(id);
	}

	public async Task<PagedList<Livro>?> GetLivroByUsernameAsync(PaginationParams pgaParams, string userId)
	{
		var livros = _context.Livros.Where(c => c.AppUser != null && c.AppUserId == userId);
		return await PagedList<Livro>.CreateAsync(livros, pgaParams.PageNumber, pgaParams.PageSize);
	}

	public async Task<PagedList<Livro>> GetLivrosAsync(PaginationParams pgaParams)
	{
		var livros = _context.Livros;
		return await PagedList<Livro>.CreateAsync(livros, pgaParams.PageNumber, pgaParams.PageSize);
	}

	public async Task<List<Livro>> GetLivrosAsync()
	{
		return await _context.Livros.ToListAsync();
	}

	public void UpdateLivro(Livro livro)
	{
		_context.Livros.Update(livro);
	}
}
