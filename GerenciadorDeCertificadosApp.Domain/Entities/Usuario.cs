using GerenciadorDeCertificadosApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeCertificadosApp.Domain.Entities
{
    public class Usuario : IBaseEntity
    {
        public string NomeUsuario { get; private set; } = string.Empty;

        public string Email { get; private set; } = string.Empty;

        public string Senha { get; private set; } = string.Empty;

        public Perfil Perfil { get; private set; }


        public List<Certificado> Certificados { get; private set; } = new();


        // ✅ Construtor privado usado pelo EF
        private Usuario()
        {

        }

        // ✅ Construtor para usar no Mapper para gerar Usuarios iniciais
        public Usuario(Guid id, string nomeUsuario, string email, string senha, Perfil perfil)
        {
            Id = id;
            NomeUsuario = nomeUsuario;
            Email = email;
            Senha = senha;
            Perfil = perfil;
        }

        // ✅ Construtor usado pela aplicação (DTO → Entidade)
        public Usuario(string nomeUsuario, string email, string senha, int perfil)
        {
            ValidarDadosRequest(nomeUsuario, email, senha, perfil);

            Id = Guid.NewGuid();
            NomeUsuario = nomeUsuario;
            Email = email;
            Senha = senha;
            Perfil = (Perfil)perfil;
        }

        public void AtualizarDados(string nomeUsuario, string email, string senha, Perfil perfil)
        {
            NomeUsuario = nomeUsuario;
            Email = email;
            Senha = senha;
            Perfil = perfil;
        }

        private void ValidarDadosRequest(string nomeUsuario, string email, string senha, int perfil)
        {
            if (string.IsNullOrEmpty(nomeUsuario))
                throw new ApplicationException("O nome do usuário deve ser informado.");

            if (string.IsNullOrEmpty(email))
                throw new ApplicationException("O e-mail do usuário deve ser informado.");

            if(string.IsNullOrEmpty(senha))
                throw new ApplicationException("A senha do usuário deve ser informada.");

            if (!Enum.IsDefined(typeof(Perfil), perfil))
                throw new ApplicationException("O perfil do usuário informado é inválido!");
        }
    }
}
