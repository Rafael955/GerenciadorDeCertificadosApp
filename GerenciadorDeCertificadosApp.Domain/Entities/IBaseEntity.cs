namespace GerenciadorDeCertificadosApp.Domain.Entities
{
    public abstract class IBaseEntity
    {
        public Guid Id { get; protected set; }
    }
}
