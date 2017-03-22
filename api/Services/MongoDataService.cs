using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Data;
using Core.Interfaces;
using Data.Mongo;

namespace Services
{
    internal abstract class MongoDataService<TEntity, TRepository> : IDataService<TEntity>
        where TEntity: class, IEntity, new()
        where TRepository: IRepository<TEntity>
    {
        protected TRepository Repository;
        protected MongoDataContext Context;
        protected IWebApplicationConfig Config;

        protected MongoDataService(IWebApplicationConfig config)
        {
            Config = config;
            Context = new MongoDataContext(Config);
            Initialize();
        }
        public TEntity GetById(Guid id)
        {
            return Repository.Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Repository.GetAll();
        }

        public void Add(TEntity item)
        {
            Repository.Add(item);
        }

        public void Add(IEnumerable<TEntity> items)
        {
            Repository.Add(items);
        }

        public void Update(TEntity item)
        {
            Repository.Update(item);
        }

        public void Delete(Expression<Func<TEntity, bool>> expression)
        {
            Repository.Delete(expression);
        }

        public void DeleteAll()
        {
            Repository.DeleteAll();
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return Repository.Where(expression);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> expression)
        {
            return Repository.Single(expression);
        }

        protected abstract void Initialize(out TRepository repository);

        private void Initialize()
        {
            Initialize(out Repository);
        }
    }
}