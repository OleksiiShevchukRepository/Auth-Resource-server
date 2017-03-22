using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Data;
using Core.Interfaces;
using Data.MSSQL;

namespace Services
{
    public abstract class SqlDataService<TEntity, TRepository> : IDataService<TEntity> 
        where TEntity : class, IEntity, new()
        where TRepository : IRepository<TEntity>
    {
        protected TRepository Repository;
        protected IWebApplicationConfig Config;
        protected SqlUnitOfWork Context;

        protected SqlDataService(IWebApplicationConfig config)
        {
            Config = config;
            Context = new SqlUnitOfWork(Config);
            Initialize();
        }

        protected abstract void Initialize(out TRepository repository);

        private void Initialize()
        {
            Initialize(out Repository);
        }
        public void Add(TEntity item)
        {
            Repository.Add(item);
        }

        public void Add(IEnumerable<TEntity> items)
        {
            Repository.Add(items);
        }

        public void Delete(Expression<Func<TEntity, bool>> expression)
        {
            Repository.Delete(expression);
        }

        public void DeleteAll()
        {
            Repository.DeleteAll();
        }

        public IQueryable<TEntity> GetAll()
        {
           return Repository.GetAll();
        }

        public TEntity GetById(Guid id)
        {
            return Repository.Find(id);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> expression)
        {
            return Repository.Single(expression);
        }

        public void Update(TEntity item)
        {
            Repository.Update(item);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return Repository.Where(expression);
        }
    }
}