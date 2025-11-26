using GerenciadorDeCertificadosApp.Domain.Entities;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Repositories;
using GerenciadorDeCertificadosApp.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace GerenciadorDeCertificadosApp.Infra.Data.Repositories
{
    public class CertificadosRepository : BaseRepository<Certificado>, ICertificadosRepository
    {
        private readonly DataContext _context;

        public CertificadosRepository(DataContext context, IHostEnvironment env) : base(context, env)
        {
            _context = context;
        }

        public override Certificado? GetById(Guid id)
        {
            var certificado = _context.Set<Certificado>()
                .Include(c => c.Atividades!) // Aplicar o operador de supressão de nulabilidade (`!`) para indicar ao compilador que `Atividades` não é nula no momento do Include.
                    .ThenInclude(ca => ca.Atividade)
                .Include(c => c.Usuario)
                .FirstOrDefault(c => c.Id == id);

            return certificado;
        }

        public List<Certificado>? GetByUserId(Guid userId)
        {
            var certificado = _context.Set<Certificado>()
                .AsNoTracking()
                .Where(c => c.UsuarioId == userId).ToList();

            return certificado;
        }
    }
}
