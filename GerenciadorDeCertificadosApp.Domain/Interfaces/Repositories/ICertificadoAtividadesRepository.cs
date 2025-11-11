using GerenciadorDeCertificadosApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeCertificadosApp.Domain.Interfaces.Repositories
{
    public interface ICertificadoAtividadesRepository
    {
        void AddCertificateWithActivities(CertificadoAtividade certificadoAtividade);

        void RemoveActivitiesFromCertificate(Guid certificadoId);

        bool IsActivityLinkedToAnyCertificate(Guid atividadeId);

        bool IsActivityAlreadyIncludedInTheCertificate(Guid atividadeId, Guid certificadoId);
    }
}
