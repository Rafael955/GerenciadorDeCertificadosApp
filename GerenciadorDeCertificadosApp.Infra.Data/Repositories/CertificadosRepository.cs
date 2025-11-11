using GerenciadorDeCertificadosApp.Domain.Entities;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Repositories;
using GerenciadorDeCertificadosApp.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeCertificadosApp.Infra.Data.Repositories
{
    public class CertificadosRepository : BaseRepository<Certificado>, ICertificadosRepository
    {
        private readonly DataContext _context;

        public CertificadosRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override Certificado? GetById(Guid id)
        {
            var certificado = _context.Set<Certificado>()
                .Include(c => c.Atividades)
                    .ThenInclude(ca => ca.Atividade)
                .Include(c => c.Usuario)
                .FirstOrDefault(c => c.Id == id);

            return certificado;
        }
    }
}
