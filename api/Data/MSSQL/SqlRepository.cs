using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Core.Data;
using Core.Interfaces;

namespace Data.MSSQL
{
    internal class SqlRepository<TEntity>: ISqlRepository<TEntity> where TEntity: class, IEntity, new()
    {

        private readonly SqlDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public SqlRepository(SqlDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public TEntity Find(Guid id)
        {
            return _dbSet.SingleOrDefault(a => a.Id == id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public void Add(TEntity item)
        {
            var entry = _context.Entry(item);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                _dbSet.Add(item);
            }

        }

        public void Add(IEnumerable<TEntity> items)
        {
            _dbSet.AddRange(items);
        }

        public void Update(TEntity entity)
        {
            var properties =
                   entity.GetType()
                         .GetProperties()
                         .Where(p => p.PropertyType.GetInterface(typeof(IEntity).Name, true) != null ||
                                     p.PropertyType.GetInterface(typeof(IEnumerable).Name, true) != null);

            foreach (var prop in properties)
            {
                var nestedProperty = prop.GetValue(entity);

                if (nestedProperty is IEnumerable)
                {
                    foreach (var item in (IEnumerable)nestedProperty)
                    {
                        if (!(item is IEntity))
                        {
                            break;
                        }

                        DbEntityEntry nestedEntry = _context.Entry(item);

                        if (nestedEntry.State == EntityState.Detached)
                        {
                            _context.Set(item.GetType()).Attach(item);
                        }

                        nestedEntry.State = EntityState.Modified;
                    }
                }
                else
                {
                    if (nestedProperty != null)
                    {
                        DbEntityEntry nestedEntry = _context.Entry(nestedProperty);

                        if (nestedEntry.State == EntityState.Detached)
                        {
                            _context.Set(nestedProperty.GetType()).Attach(nestedProperty);
                        }

                        nestedEntry.State = EntityState.Modified;
                    }
                }
            }
        }

        public void Delete(Expression<Func<TEntity, bool>> expression)
        {
            var entities = _dbSet.Where(expression);
            _dbSet.RemoveRange(entities);
        }

        public void DeleteAll()
        {
            foreach (var item in _dbSet)
            {
                _dbSet.Remove(item);
            }
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expresson)
        {
            return _dbSet.Where(expresson);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.SingleOrDefault(expression);
        }
    }
}