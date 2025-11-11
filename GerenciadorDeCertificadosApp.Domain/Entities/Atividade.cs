namespace GerenciadorDeCertificadosApp.Domain.Entities
{
    public class Atividade : IBaseEntity
    {
        public string Nome { get; private set; }

        public List<CertificadoAtividade> Certificados { get; private set; }

        // ✅ Construtor privado usado pelo EF
        private Atividade()
        {

        }

        // ✅ Construtor usado pela aplicação (DTO → Entidade)
        public Atividade(string nome)
        {
            ValidarDadosRequest(nome);

            Id = Guid.NewGuid();
            Nome = nome;
        }

        public void AtualizarDados(string nome)
        {
            ValidarDadosRequest(nome);

            Nome = nome;
        }

        private void ValidarDadosRequest(string nome)
        {
            if(string.IsNullOrEmpty(nome))
                throw new ApplicationException("O nome da atividade deve ser informado.");

            if (nome.Length > 100)
                throw new ApplicationException("O nome da atividade deve ter no máximo 100 caracteres.");

            if (nome.Length < 2)
                throw new ApplicationException("O nome da atividade deve ter no mínimo 2 caracteres.");
        }
    }
}
