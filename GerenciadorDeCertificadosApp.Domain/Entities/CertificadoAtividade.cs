using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeCertificadosApp.Domain.Entities
{
    public class CertificadoAtividade
    {
        public Guid IdCertificado { get; set; }

        public Certificado Certificado { get; set; }

        public Guid IdAtividade { get; set; }

        public Atividade Atividade { get; set; }    
    }
}
