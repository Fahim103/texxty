using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TexxtyDataAccess.Models;

namespace TexxtyDataAccess.Repository.Classes
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly TexxtyDBEntities context;

        public Repository()
        {
            this.context = new TexxtyDBEntities();
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
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
        }
        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Set<TEntity>().Remove(this.Get(id));
            context.SaveChanges();
        }
    }
}