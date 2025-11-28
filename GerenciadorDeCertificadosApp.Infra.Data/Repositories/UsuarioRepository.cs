using GerenciadorDeCertificadosApp.Domain.Entities;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Repositories;
using GerenciadorDeCertificadosApp.Infra.Data.Contexts;
using Microsoft.Extensions.Hosting;

namespace GerenciadorDeCertificadosApp.Infra.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly DataContext _context;

        public UsuarioRepository(DataContext context, IHostEnvironment env) : base(context, env)
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

        public void DeleteUserAccount(Guid id)
        {
            var user = GetById(id);
            _context.Set<Usuario>().Remove(user!);
            _context.SaveChanges();
        }
    }
}
