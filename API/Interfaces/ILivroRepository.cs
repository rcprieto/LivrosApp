using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface ILivroRepository
{
	void AddLivro(Livro livro);
	void UpdateLivro(Livro livro);
	Task<PagedList<Livro>> GetLivrosAsync(PaginationParams pgaParams);
	Task<Livro?> GetLivroById(int id);
	Task<PagedList<Livro>?> GetLivroByUsernameAsync(PaginationParams pgaParams, string userId);

}
