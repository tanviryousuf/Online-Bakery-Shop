using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TanvirBakery.Interface;
using TanvirBakery.Models;

namespace TanvirBakery.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private BakeryContext emsContext = new BakeryContext();
        public Repository(BakeryContext emsContext)
        {
            this.emsContext = emsContext;
        }

        public TEntity GetById(int id)
        {
            return emsContext.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return emsContext.Set<TEntity>().ToList();
        }

        public void Insert(TEntity entity)
        {
            emsContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            emsContext.Set<TEntity>().Remove(entity);
        }
        public int Submit()
        {
            return emsContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            emsContext.Entry(entity).State = EntityState.Modified;
        }
    }


}
