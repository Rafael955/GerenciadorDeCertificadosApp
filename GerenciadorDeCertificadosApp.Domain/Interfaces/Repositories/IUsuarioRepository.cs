using GerenciadorDeCertificadosApp.Domain.Entities;

namespace GerenciadorDeCertificadosApp.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Usuario? GetUserByEmailAndPassword(string email, string senha);

        bool Any(string email);

        void DeleteUserAccount(Guid id);
    }
}
