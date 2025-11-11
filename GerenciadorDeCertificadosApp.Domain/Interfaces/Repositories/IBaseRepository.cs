namespace GerenciadorDeCertificadosApp.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        void Add(T objeto);

        void Update(T objeto);

        void Delete(T objeto);

        T? GetById(Guid id);

        List<T>? GetAll();
    }
}
