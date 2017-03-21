using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Data;
using Core.Interfaces;
using MongoDB.Driver;

namespace Data.Mongo
{
    internal class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IMongoCollection<TEntity> _mongoCollection;

        public MongoRepository(IMongoDatabase database)
        {
            _mongoCollection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }
        public TEntity Find(Guid id)
        {
            return _mongoCollection.Find(a => a.Id == id).SingleOrDefault();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _mongoCollection.AsQueryable();
        }

        public void Add(TEntity item)
        {
            _mongoCollection.InsertOne(item);
        }

        public void Add(IEnumerable<TEntity> items)
        {
            _mongoCollection.InsertMany(items);
        }

        public void Update(TEntity item)
        {
            _mongoCollection.ReplaceOne(i => i.Id == item.Id, item);
        }

        public void Delete(Expression<Func<TEntity, bool>> expression)
        {
            _mongoCollection.DeleteMany(expression);
        }

        public void DeleteAll()
        {
            _mongoCollection.DeleteMany(_=>true);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
           return _mongoCollection.AsQueryable().Where(expression);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> expression)
        {
            return _mongoCollection.AsQueryable().SingleOrDefault(expression);
        }
    }
}