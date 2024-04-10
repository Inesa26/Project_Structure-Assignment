using Domain;

namespace Application
{
    public interface IRepository<T> where T : Entity
    {
        T Get(Guid id);
        List<T> GetAll();
        T Delete(Guid id);
        T Add(T entity);
        void Update(Guid id, T entity);
    }
}
