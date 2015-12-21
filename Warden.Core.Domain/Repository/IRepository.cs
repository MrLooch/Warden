using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Warden.Core.Domain.Repository;
using Warden.DataModel.Entities;

namespace Warden.Core.Domain
{
  
    /// <summary>
    /// The repository interface.
    /// </summary>
    /// <typeparam name="T">
    /// The domain entity
    /// </typeparam>
    public interface IRepository<T> : IReadOnlyRepository<T>
        where T : IEntity
    {
        Task CreateIndex(Expression<Func<T, object>> expression);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        Task AddAsync(T entity,
                        CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Add an array of entities.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="ordered">   /// Optional. If true, perform an ordered insert of the documents in the array, and if an error occurs with one of documents, MongoDB will return without processing the remaining documents in the array.
        /// If false, perform an unordered insert, and if an error occurs with one of documents, continue processing the remaining documents in the array.
        /// Defaults to true.</param>
        Task AddManyAsync(IEnumerable<T> entities,
                            bool ordered = true,
                            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(int id,
                                CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        Task<bool> RemoveAsync(T entity,
                                CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Delete all the entries in the repository.
        /// </summary>
        Task<bool> RemoveManyAsync(System.Linq.Expressions.Expression<Func<T, bool>> filterCondition,
                                    CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update one entity with the field for specific that fullfil the filter condition
        /// </summary>
        /// <typeparam name="TField">The type of the field to update</typeparam>        
        /// <param name="fieldQuery">The field to update in the entity</param>
        /// <param name="filterCondition">The filter conditon to select the entity in the collection</param>
        /// <param name="fieldUpdateValue">The specific </param>
        /// <param name="isUpSert">Flag indicating whether to insert the document if it doesn't exist</param>
        /// <returns></returns>
        Task<bool> UpdateOneAsync<TField>(System.Linq.Expressions.Expression<Func<T, TField>> fieldQuery,
                                            System.Linq.Expressions.Expression<Func<T, bool>> filterCondition,
                                            TField fieldUpdateValue,
                                            bool isUpSert,
                                            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Update many entities with the field for specific that fullfil the filter condition
        /// </summary>
        /// <typeparam name="TField">The type of the field to update</typeparam>        
        /// <param name="fieldQuery">The field to update in the entity</param>
        /// <param name="filterCondition">The filter conditon to select the entity in the collection</param>
        /// <param name="fieldUpdateValue">The specific </param>
        /// <param name="isUpSert">Flag indicating whether to insert the document if it doesn't exist</param>
        /// <returns></returns>
        Task<bool> UpdateManyAsync<TField>(System.Linq.Expressions.Expression<Func<T, TField>> fieldQuery,
                                            System.Linq.Expressions.Expression<Func<T, bool>> filterCondition,
                                            TField fieldUpdateValue,
                                            bool isUpSert,
                                            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Replace a single document
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filterQuery"></param>
        /// <returns></returns>
        Task<bool> ReplaceOneAsync(T entity,
                                    System.Linq.Expressions.Expression<Func<T, bool>> filterQuery,
                                    CancellationToken cancellationToken = default(CancellationToken));        
    }
}
