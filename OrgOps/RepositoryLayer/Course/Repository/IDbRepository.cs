using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public interface IDbRepository<T>: IBaseRepository<T>
    {
        /// <summary>
        /// Any recors there or not checks
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<bool> Any(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);
        /// <summary>
        /// The GetAsync.
        /// </summary>
        /// <param name="filter">The filter<see cref="Expression{Func{T, bool}}"/>.</param>
        /// <param name="orderBy">The orderBy<see cref="Func{IQueryable{T}, IOrderedQueryable{T}}"/>.</param>
        /// <param name="includes">The includes<see cref="Expression{Func{T, object}}[]"/>.</param>
        /// <returns>The <see cref="IEnumerable{T}"/>.</returns>
        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// The GetAsync Group By
        /// </summary>
        /// <param name="grpFilter"></param>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<IEnumerable<IGrouping<Tkey, T>>> GetAsyncGroupBy<Tkey>(Func<T, Tkey> grpFilter, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// The GetFirstOrDefaultAsync.
        /// </summary>
        /// <param name="filter">The filter<see cref="Expression{Func{T, bool}}"/>.</param>
        /// <param name="orderBy">The orderBy<see cref="Func{IQueryable{T}, IOrderedQueryable{T}}"/>.</param>
        /// <param name="includes">The includes<see cref="Expression{Func{T, object}}[]"/>.</param>
        /// <returns>The <see cref="IEnumerable{T}"/>.</returns>
        Task<T> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// The GetFirstOrDefaultAsync Groyup By
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetFirstOrDefaultAsyncGroupBy<Tkey>(Func<T, Tkey> grpFilter, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);



        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        void Insert(T entity);

        /// <summary>
        /// The InsertRange.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        void InsertRange(IEnumerable<T> entities);

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        void Update(T entity);

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        void Delete(T entity);

        /// <summary>
        /// The DeleteRange.
        /// </summary>
        /// <param name="entities">The entities<see cref="ICollection"/>.</param>
        /// <summary>
        /// The DeleteRange.
        /// </summary>
        /// <param name="filter"></param>
        bool DeleteRange(Expression<Func<T, bool>> filter);

        /// <summary>
        /// The UpdateRange.
        /// </summary>
        /// <param name="entities">The entities<see cref="ICollection"/>.</param>
        void UpdateRange(IEnumerable<T> entities);
    }
}
