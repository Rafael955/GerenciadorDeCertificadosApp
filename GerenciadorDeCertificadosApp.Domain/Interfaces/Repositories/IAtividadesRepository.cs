using GerenciadorDeCertificadosApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeCertificadosApp.Domain.Interfaces.Repositories
{
    public interface IAtividadesRepository : IBaseRepository<Atividade>
    {
        Atividade? GetByName(string nome);
    }
}
