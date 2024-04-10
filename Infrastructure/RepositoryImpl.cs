using Application;
using Domain;

namespace Infrastructure
{
    public class RepositoryImpl<T> : IRepository<T> where T : Entity
    {
        private readonly List<T> _entities;
        public RepositoryImpl()
        {
            _entities = new List<T>();
        }

        public T Add(T entity)
        {
            _entities.Add(entity);
            return entity;
        }

        public T? Delete(Guid id)
        {
            T entity = null;
            for (int i = 0; i < _entities.Count; i++)
            {
                if (_entities[i].Id == id)
                {
                    entity = _entities[i];
                    _entities.RemoveAt(i);
                    break;
                }
            }
            return entity;
        }

        public T Get(Guid id)
        {
            foreach (var entity in _entities)
            {
                if (entity.Id == id)
                {
                    return entity;
                }
            }
            return null;
        }

        public List<T> GetAll()
        {
            return _entities;
        }

        public void Update(Guid id, T entity)
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                if (_entities[i].Id == id)
                {
                    _entities[i] = entity;
                    break;
                }
            }
        }
    }

}
