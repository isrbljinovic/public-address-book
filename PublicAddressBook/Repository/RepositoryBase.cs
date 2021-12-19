using System;
using System.Linq;
using System.Linq.Expressions;
using Contracts.Repository;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DataBaseContext DataBaseContext;

        public RepositoryBase(DataBaseContext dataBaseContext)
        {
            DataBaseContext = dataBaseContext;
        }

        public void Create(T entity)
        {
            DataBaseContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            DataBaseContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            return trackChanges ? DataBaseContext.Set<T>()
                : DataBaseContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return trackChanges ? DataBaseContext.Set<T>().Where(expression)
                : DataBaseContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            DataBaseContext.Set<T>().Update(entity);
        }
    }
}