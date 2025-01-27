using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Auction.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // Get all entities with optional filtering and included properties
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null
        );

        // Get a single entity based on a filter with optional included properties
        T Get(
            Expression<Func<T, bool>> filter,
            string? includeProperties = null
        );

        // Add a new entity
        void Add(T entity);

        // Remove a single entity
        void Remove(T entity);

        // Remove a range of entities
        void RemoveRange(IEnumerable<T> entity);
    }
}
