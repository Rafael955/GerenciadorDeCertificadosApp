using GerenciadorDeCertificadosApp.Domain.Entities;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Repositories;
using GerenciadorDeCertificadosApp.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace GerenciadorDeCertificadosApp.Infra.Data.Repositories
{
    public class AtividadesRepository : BaseRepository<Atividade>, IAtividadesRepository
    {
        private readonly DataContext _context;

        public AtividadesRepository(DataContext context, IHostEnvironment env) : base(context, env)
        {
            _context = context;
        }

        public override Atividade? GetById(Guid id)
        {
            return _context.Set<Atividade>()
                .Include(a => a.Certificados)
                .SingleOrDefault(a => a.Id == id);
        }

        public Atividade? GetByName(string nome)
        {
            return _context.Set<Atividade>()
                .Where(a => a.Nome.ToUpper() == nome.ToUpper())
                .SingleOrDefault();
        }
    }
}
