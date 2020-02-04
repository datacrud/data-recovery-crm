using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Project.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(Guid id);
        T GetById(string id);
        T Add(T entity);
        EntityState Edit(T entity);
        T Delete(T entity);

        bool Commit();
        IQueryable<T> Add(List<T> entities);
    }



    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity: class
    {
        protected DbContext DbContext;

        protected BaseRepository(DbContext db)
        {
            DbContext = db;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>().AsQueryable();
        }

        public virtual TEntity GetById(Guid id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public virtual TEntity GetById(string id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public virtual TEntity Add(TEntity entity)
        {
            return DbContext.Set<TEntity>().Add(entity);
        }

        public virtual IQueryable<TEntity> Add(List<TEntity> entities)
        {
            return DbContext.Set<TEntity>().AddRange(entities).AsQueryable();
        }

        public virtual EntityState Edit(TEntity entity)
        {
            return DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual TEntity Delete(TEntity entity)
        {
            return DbContext.Set<TEntity>().Remove(entity);
        }        

        public bool Commit()
        {
            var saveChanges = DbContext.SaveChanges();

            return saveChanges > 0;
        }
    }

    
}
