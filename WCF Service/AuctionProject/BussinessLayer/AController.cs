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
    public abstract class AController<T>  where T : class
    {
        public ARepository<T> Repository { get; set; }

        public AController()
        {
        }

        public AController(ARepository<T> auctionRepository)
        {
            this.Repository = auctionRepository;
        }

        public virtual T GetByIdWithObjects(int Id)
        {
           return Repository.getByIdWithObjects(Id);
        }
        
        public virtual int Add(T entity)
        {
            return Repository.Add(entity);
        }

       
        public virtual ICollection<T> getAll()
        { 
            return Repository.GetAll();
        }

        public virtual Task<List<T>> GetAllAsync()
        {
            return Repository.GetAllAsync();
        }

        public virtual T getById(int id)
        {
            return Repository.GetById(id);
        }

        public virtual int Remove(T entity)
        {
           return Repository.Remove(entity);
        }

        public virtual int RemoveById(int id)
        {
           return Repository.RemoveById(id);
        }

        public virtual int Update(T entity)
        {
           return Repository.Update(entity);
        }
    }
}
