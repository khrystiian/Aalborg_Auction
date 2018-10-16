using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Core;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Data.Entity.Migrations;
using System.Diagnostics;

namespace Database
{
    public abstract class ARepository<T> where T : class
    {
        public Dbcontext context { get; set; }
        public DbSet<T> Set;

        public ARepository(Dbcontext Context)
        {
            this.context = Context;
            Set = context.Set<T>();
        }

        public ARepository()
        {
            context = new Dbcontext();
            Set = context.Set<T>();
        }

        public abstract T getByIdWithObjects(int Id);

        public virtual int Save()
        {
              return context.SaveChanges();
        }

        public virtual int Add(T entity)
        {
            Set.Add(entity);
            return Save();
        }


        public virtual ICollection<T> GetAll()
        {
            return Set.ToList();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
           return await Set.ToListAsync();
        }

        public virtual T GetById(int id)
        {
            return Set.Find(id);
        }

        public virtual int Remove(T entity)
        {
            Set.Remove(entity);
            return Save();
        }

        public virtual int RemoveById(int id)
        { 
            Set.Remove(Set.Find(id));
            return Save();
        }

        public virtual int Update(T entity)
        {
            Set.Attach(entity);
            var entry = context.Entry(entity).State = EntityState.Modified;
            return Save();
        }
    }
}
