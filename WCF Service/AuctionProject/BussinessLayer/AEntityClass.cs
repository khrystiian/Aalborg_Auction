using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Core;
using System.Data.Entity;

namespace BussinessLayer
{
    public abstract class AEntityClass<T> where T : class
    {
        public ARepository<T> repository { get; set; }
        
        public void Add(T entity)
        {
            repository.Add(entity);
        }

        public ICollection<T> getAll()
        {
            return repository.Set.ToList();
        }

        public T getById(int id)
        {
            return repository.Set.Find(id);
        }

        public void Remove(T entity)
        {
            repository.Set.Remove(entity);
        }

        public int Save()
        {
            return repository.Save();
        }

        public void RemoveById(int id)
        {
            repository.Set.Remove(repository.Set.Find(id));
        }

        public void Update(T entity)
        {
            repository.context.Entry(entity).CurrentValues.SetValues(entity);
        }
    }
}
