using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IDataService<TEntity> where TEntity: class, IEntity, new()
    {
        TEntity GetById(Guid id);
        IQueryable<TEntity> GetAll();
        void Add(TEntity item);
        void Add(IEnumerable<TEntity> items);
        void Update(TEntity item);
        void Delete(Expression<Func<TEntity, bool>> expression);
        void DeleteAll();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression);
        TEntity Single(Expression<Func<TEntity, bool>> expression);
    }
}