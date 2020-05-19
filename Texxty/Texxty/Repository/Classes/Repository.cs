using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Texxty.Models;

namespace Texxty.Repository.Classes
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly TexxtyDBEntitiesMVC context;

        public Repository()
        {
            this.context = new TexxtyDBEntitiesMVC();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }
        
        public TEntity Get(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public void Insert(TEntity entity)
        {

            try
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
            catch ( Exception e)
            {
                var msg = e.InnerException;
            }
        }
        public void Update(TEntity entity)
        {
           try
           {  
                
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
           }
           catch (Exception e)
           {
              var msg = e.InnerException;
           }

        }

        public void Delete(int id)
        {
            context.Set<TEntity>().Remove(this.Get(id));
            context.SaveChanges();
        }
    }
}