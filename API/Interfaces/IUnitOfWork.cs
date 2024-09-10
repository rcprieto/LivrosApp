using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAppUserRepository AppUserRepository { get; }
        ILivroRepository LivroRepository { get; }


        Task<bool> Complete();
        bool HasChange();
    }
}
