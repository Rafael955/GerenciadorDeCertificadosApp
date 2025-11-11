using GerenciadorDeCertificadosApp.Domain.Entities;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Repositories;
using GerenciadorDeCertificadosApp.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeCertificadosApp.Infra.Data.Repositories
{
    public class CertificadoAtividadesRepository : ICertificadoAtividadesRepository
    {
        private readonly DataContext _context;

        public CertificadoAtividadesRepository(DataContext context)
        {
            _context = context;
        }

        public void AddCertificateWithActivities(CertificadoAtividade certificadoAtividade)
        {
            _context.Add(certificadoAtividade);
            _context.SaveChanges();
        }

        public void RemoveActivitiesFromCertificate(Guid certificadoId)
        {
            // Carrega as entidades e remove via ChangeTracker para manter o estado em memória consistente
            var entries = _context.Set<CertificadoAtividade>()
                .Where(ca => ca.IdCertificado == certificadoId)
                .ToList();

            if (entries.Any())
            {
                _context.RemoveRange(entries);
                _context.SaveChanges();
            }
        }

        public bool IsActivityLinkedToAnyCertificate(Guid atividadeId)
        {
            return _context.Set<CertificadoAtividade>()
                .Any(ca => ca.IdAtividade == atividadeId);
        }

        public bool IsActivityAlreadyIncludedInTheCertificate(Guid atividadeId, Guid certificadoId)
        {
            return _context.Set<CertificadoAtividade>()
                .Any(ca => ca.IdAtividade == atividadeId && ca.IdCertificado == certificadoId);
        }
    }
}
