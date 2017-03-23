using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Data;

namespace Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        TEntity Find(Guid id);
        IQueryable<TEntity> GetAll();
        void Add(TEntity item);
        void Add(IEnumerable<TEntity> items);
        void Update(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> expression);
        void DeleteAll();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expresson);
        TEntity Single(Expression<Func<TEntity, bool>> expression);
    }

    public interface IMongoRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new() { }

    public interface ISqlRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {}
}