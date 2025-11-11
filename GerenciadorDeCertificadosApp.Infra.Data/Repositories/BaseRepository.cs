using GerenciadorDeCertificadosApp.Domain.Entities;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Repositories;
using GerenciadorDeCertificadosApp.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeCertificadosApp.Infra.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : IBaseEntity
    {
        private readonly DataContext _context;

        public BaseRepository(DataContext context)
        {
            _context = context;
        }

        public virtual void Add(T objeto)
        {
            _context.Add(objeto);
            _context.SaveChanges();
        }

        public virtual void Update(T objeto)
        {
            _context.Update(objeto);
            _context.SaveChanges();
        }

        public virtual void Delete(T objeto)
        {
            _context.Remove(objeto);
            _context.SaveChanges();
        }

        public virtual T? GetById(Guid id)
        {
            return _context.Set<T>()
                    .SingleOrDefault(c => c.Id == id);
        }

        public virtual List<T>? GetAll()
        {
            return _context.Set<T>()
                     .AsNoTracking()
                     .ToList();
        }
    }
}
