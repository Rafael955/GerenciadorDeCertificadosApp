using GerenciadorDeCertificadosApp.Domain.Entities;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Repositories;
using GerenciadorDeCertificadosApp.Infra.Data.Contexts;

namespace GerenciadorDeCertificadosApp.Infra.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly DataContext _context;

        public UsuarioRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public Usuario? GetUserByEmailAndPassword(string email, string senha)
        {
            return _context.Set<Usuario>()
                .SingleOrDefault(u => u.Email == email && u.Senha == senha);
        }

        public bool Any(string email)
        {
            return _context.Set<Usuario>()
                .Any(u => u.Email == email);
        }
    }
}
