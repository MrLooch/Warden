using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Warden.Core.Domain;
using Warden.DataModel.Entities;
using MongoDB.Driver;
using Warden.DataService.Core.Connection;
using Warden.Core.Domain.Exceptions;

namespace Warden.DataService.Core.Repository
{
    public class RepositoryMongoDB<T> : IRepository<T> where T : EntityBase
    {
        private string collectionName;
        private  ConnectionConfig connection;
        /// <summary>
        /// 
        /// </summary>
        public RepositoryMongoDB(ConnectionConfig connection,
                                 string collectionName)
        {
            this.connection = connection;
            this.collectionName = collectionName;
        }

        /// <summary>
        /// Create an index based on a single field record
        /// </summary>
        /// <param name="expression"></param>
        public async Task CreateIndex(Expression<Func<T, object>> expression)
        {
            try
            {
                if (isConnected())
                {
                    IMongoCollection<T> collection = getCollection();
                    CreateIndexOptions indexOptions = new CreateIndexOptions() { Unique = false };
                    await collection.Indexes.CreateOneAsync(Builders<T>.IndexKeys.Ascending(expression), indexOptions);
                }
            }
            catch (Exception e)
            {
                throw new DatabaseConnectionException(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private IMongoCollection<T> getCollection()
        {
            return this.connection.Database.GetCollection<T>(this.collectionName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool isConnected()
        {
            return this.connection != null && this.connection.isConnected();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public async Task AddAsync(T entity,
                                   CancellationToken cancellationToken = default(CancellationToken))
        {
            Task addTask = Task.FromResult(false);
            try
            {
                if (isConnected())
                {
                    IMongoCollection<T> entities = getCollection();

                    await entities.InsertOneAsync(entity, cancellationToken);


                }
            }
            catch (Exception t)
            {
                throw new DatabaseConnectionException(t.Message);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ordered">
        /// Optional. If true, perform an ordered insert of the documents in the array, 
        /// and if an error occurs with one of documents, MongoDB will return without processing 
        /// the remaining documents in the array.
        /// If false, perform an unordered insert, and if an error occurs with one of documents, 
        /// continue processing the remaining documents in the array.
        /// Defaults to true.</param>
        public async Task AddManyAsync(IEnumerable<T> entities,
                                 bool ordered = true,
                                 CancellationToken cancellationToken = default(CancellationToken))
        {
            if (entities.Count() > 0 && isConnected())
            {
                try
                {
                    IMongoCollection<T> collection = getCollection();

                    await collection.InsertManyAsync(entities,
                                                     new InsertManyOptions() { IsOrdered = ordered },
                                                     cancellationToken);
                }
                catch (Exception t)
                {
                    throw new DatabaseConnectionException(t.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(Guid id,
                                            CancellationToken cancellationToken = default(CancellationToken))
        {
            bool removed = false;
            if (isConnected())
            {
                try
                {
                    IMongoCollection<T> collection = getCollection();

                    var result = await collection.DeleteOneAsync(p => p.Id == id, cancellationToken);
                    removed = result.IsAcknowledged && result.DeletedCount > 0;
                }
                catch (Exception t)
                {
                    throw new DatabaseConnectionException(t.Message);
                }
            }
            return removed;
        }

        /// <summary>
        /// Remove a single document
        /// </summary>
        /// <param name="entity"></param>
        public async Task<bool> RemoveAsync(T entity,
                                            CancellationToken cancellationToken = default(CancellationToken))
        {
            bool removed = false;
            if (isConnected())
            {
                try
                {
                    IMongoCollection<T> collection = getCollection();

                    var result = await collection.DeleteOneAsync(p => p.Id == entity.Id, cancellationToken);
                    removed = result.IsAcknowledged && result.DeletedCount > 0;
                }
                catch (Exception t)
                {
                    throw new DatabaseConnectionException(t.Message);
                }
            }
            return removed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        public async Task<bool> RemoveManyAsync(System.Linq.Expressions.Expression<Func<T, bool>> filterCondition,
                                                CancellationToken cancellationToken = default(CancellationToken))
        {
            bool removed = false;
            if (isConnected())
            {
                try
                {
                    IMongoCollection<T> collection = getCollection();
                    var result = await collection.DeleteManyAsync(filterCondition, cancellationToken);
                    removed = result.IsAcknowledged && result.DeletedCount > 0;
                }
                catch (Exception t)
                {
                    throw new DatabaseConnectionException(t.Message);
                }

            }
            return removed;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="fieldQuery"></param>
        /// <param name="filterCondition"></param>
        /// <param name="fieldUpdateValue"></param>
        /// <param name="isUpSert"></param>
        /// <returns></returns>
        public async Task<bool> UpdateOneAsync<TField>(Expression<Func<T, TField>> fieldQuery,
                                                 Expression<Func<T, bool>> filterCondition,
                                                 TField fieldUpdateValue,
                                                 bool isUpSert,
                                                 CancellationToken cancellationToken = default(CancellationToken))
        {
            bool updated = false;
            if (isConnected())
            {
                try
                {
                    IMongoCollection<T> collection = getCollection();

                    // Set the field with the new value
                    UpdateDefinition<T> updateDef = Builders<T>.Update.Set<TField>(fieldQuery, fieldUpdateValue);

                    // Search for the documents to update
                    FilterDefinition<T> filterDef = Builders<T>.Filter.Where(filterCondition);

                    // If the document doesn't exist just add it.
                    UpdateOptions opt = new UpdateOptions()
                    {
                        IsUpsert = isUpSert
                    };
                    var result = await collection.UpdateOneAsync<T>(filterCondition, updateDef, opt, cancellationToken);
                    updated = result.IsAcknowledged && result.ModifiedCount > 0;
                }
                catch (Exception t)
                {
                    throw new DatabaseConnectionException(t.Message);
                }
            }
            return updated;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="fieldQuery"></param>
        /// <param name="filterCondition"></param>
        /// <param name="fieldUpdateValue"></param>
        /// <param name="isUpSert"></param>
        /// <returns></returns>
        public async Task<bool> UpdateManyAsync<TField>(Expression<Func<T, TField>> fieldQuery,
                                                        Expression<Func<T, bool>> filterCondition, TField fieldUpdateValue,
                                                        bool isUpSert,
                                                        CancellationToken cancellationToken = default(CancellationToken))
        {
            bool updated = false;
            if (isConnected())
            {
                try
                {
                    IMongoCollection<T> collection = getCollection();

                    // Set the field with the new value
                    UpdateDefinition<T> updateDef = Builders<T>.Update.Set<TField>(fieldQuery, fieldUpdateValue);

                    // Search for the documents to update
                    FilterDefinition<T> filterDef = Builders<T>.Filter.Where(filterCondition);

                    // If the document doesn't exist just add it.
                    UpdateOptions opt = new UpdateOptions()
                    {
                        IsUpsert = isUpSert
                    };
                    var result = await collection.UpdateManyAsync<T>(filterCondition, updateDef, opt, cancellationToken);
                    updated = result.IsAcknowledged && result.ModifiedCount > 0;
                }
                catch (Exception t)
                {
                    throw new DatabaseConnectionException(t.Message);
                }
            }
            return updated;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filterQuery"></param>
        /// <returns></returns>
        public async Task<bool> ReplaceOneAsync(T entity,
                                                Expression<Func<T, bool>> filterQuery,
                                                CancellationToken cancellationToken = default(CancellationToken))
        {
            bool isReplaced = false;
            if (isConnected())
            {
                try
                {
                    IMongoCollection<T> collection = getCollection();
                    UpdateOptions opt = new UpdateOptions()
                    {
                        IsUpsert = true
                    };
                    var result = await collection.ReplaceOneAsync(filterQuery, entity, opt, cancellationToken);
                    isReplaced = result.IsAcknowledged && result.MatchedCount > 0 && result.ModifiedCount > 0;
                }
                catch (Exception t)
                {
                    throw new DatabaseConnectionException(t.Message);
                }
            }
            return isReplaced;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> FindByIdAsync(Guid id)
        {
            T findItem = default(T);

            if (isConnected())
            {
                try
                {
                    IMongoCollection<T> collection = getCollection();
                    findItem = await collection.Aggregate<T>().Match(s => s.Id == id).FirstOrDefaultAsync<T>();
                }
                catch (Exception t)
                {
                    throw new DatabaseConnectionException(t.Message);
                }
            }
            return findItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindAsync(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            IEnumerable<T> items = new List<T>();
            if (isConnected())
            {
                try
                {
                    IMongoCollection<T> collection = getCollection();
                    var result = collection.Aggregate<T>().Match(filter);
                    items = await result.ToListAsync();
                }
                catch (Exception t)
                {
                    throw new DatabaseConnectionException(t.Message);
                }
            }
            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetAllAsync()
        {
            List<T> items = new List<T>();

            if (isConnected())
            {
                try
                {
                    IMongoCollection<T> collection = getCollection();
                    items = await collection.Aggregate<T>().ToListAsync();
                }
                catch (Exception t)
                {
                    throw new DatabaseConnectionException(t.Message);
                }
            }

            return items;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> OfType<TEntity>()
        {
            IEnumerable<TEntity> items = new List<TEntity>();
            if (isConnected())
            {
                try
                {
                    IMongoCollection<T> entities = getCollection();

                    var task = await entities.Find<T>(p => p is TEntity).ToListAsync<T>();
                    items = task.Cast<TEntity>();
                }
                catch (Exception t)
                {
                    throw new DatabaseConnectionException(t.Message);
                }
            }
            return items;
        }
    }
}
