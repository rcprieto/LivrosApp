using API.Data.Migrations;
using API.Entities;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DataContext _context;
        public IAppUserRepository AppUserRepository { get; }
        public ILivroRepository LivroRepository { get; }

        public UnitOfWork(DataContext context)
        {
            this._context = context;
            this.AppUserRepository = new AppUserRepository(context);
            this.LivroRepository = new LivroRepository(context);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public async Task<bool> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync() >= 0;
            }
            catch (Exception err)
            {

                return false;

            }
        }

        public bool HasChange()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
