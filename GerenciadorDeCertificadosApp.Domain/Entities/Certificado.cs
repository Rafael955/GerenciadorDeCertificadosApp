namespace GerenciadorDeCertificadosApp.Domain.Entities
{
    public class Certificado : IBaseEntity
    {
        public string Nome { get; private set; } = string.Empty;

        public DateTime DataEmissao { get; private set; }

        #region Relacionamentos

        public List<CertificadoAtividade>? Atividades { get; private set; }


        public Guid? UsuarioId { get; private set; }

        public Usuario? Usuario { get; private set; }

        #endregion

        // ✅ Construtor privado usado pelo EF
        private Certificado()
        {

        }

        // ✅ Construtor usado pela aplicação (DTO → Entidade)
        public Certificado(string nome, Guid? usuarioId)
        {
            ValidarDadosRequest(nome, usuarioId);

            Id = Guid.NewGuid();
            Nome = nome;
            UsuarioId = usuarioId.Value;
            DataEmissao = DateTime.Now;
        }

        public void AtualizarDados(string nome, Guid? usuarioId)
        {
            ValidarDadosRequest(nome, usuarioId);

            Nome = nome;
        }

        private void ValidarDadosRequest(string nome, Guid? usuarioId)
        {
            if(string.IsNullOrEmpty(nome))
                throw new ApplicationException("O nome do aluno deve ser informado.");

            if (nome.Length > 150)
                throw new ApplicationException("O nome do aluno para o certificado deve ter no máximo 150 caracteres.");

            if (nome.Length < 3)
                throw new ApplicationException("O nome do aluno para o certificado deve ter no mínimo 3 caracteres.");

            if (usuarioId == null)
                throw new ApplicationException("O usuario responsável deve ser informado.");
        }
    }
}
