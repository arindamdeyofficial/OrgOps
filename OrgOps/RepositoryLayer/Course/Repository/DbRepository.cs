using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Repository
{
    public class DbRepository<T> : BaseRepository, IDbRepository<T>
         where T : class
    {
        private readonly DbContext _dbContext;

        /// <summary>
        /// Defines the dbSet
        /// </summary>
        private readonly DbSet<T> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="dbContext">The dbContext<see cref="DbContext"/>.</param>
        public DbRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            dbSet = dbContext?.Set<T>();
        }

        public async Task<bool> Any(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = this.dbSet;
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AnyAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// The GetAsync.
        /// </summary>
        /// <param name="filter">The filter<see cref="Expression{Func{T, bool}}"/>.</param>
        /// <param name="orderBy">The orderBy<see cref="Func{IQueryable{T}, IOrderedQueryable{T}}"/>.</param>
        /// <param name="includes">The includes<see cref="Expression{Func{T, object}}[]"/>.</param>
        /// <returns>The <see cref="IEnumerable{T}"/>.</returns>
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = this.dbSet;
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// The GetFirstOrDefaultAsync.
        /// </summary>
        /// <param name="filter">The filter<see cref="Expression{Func{T, bool}}"/>.</param>
        /// <param name="orderBy">The orderBy<see cref="Func{IQueryable{T}, IOrderedQueryable{T}}"/>.</param>
        /// <param name="includes">The includes<see cref="Expression{Func{T, object}}[]"/>.</param>
        /// <returns>The <see cref="IEnumerable{T}"/>.</returns>
        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = this.dbSet;
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// The GetAsync Group By
        /// </summary>
        /// <param name="grpFilter"></param>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<IEnumerable<IGrouping<Tkey, T>>>  GetAsyncGroupBy<Tkey>(Func<T, Tkey> grpFilter, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = this.dbSet;
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return (await query.ToListAsync()).GroupBy<T, Tkey>(x => grpFilter.Invoke(x));
        }

        /// <summary>
        /// The GetFirstOrDefaultAsync Groyup By
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetFirstOrDefaultAsyncGroupBy<Tkey>(Func<T, Tkey> grpFilter, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = this.dbSet;
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return (await query.ToListAsync()).GroupBy<T, Tkey>(x => grpFilter.Invoke(x)).Select(g => g.First());
        }

        /// <summary>
        /// The Insert.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// The InsertRange.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{T}"/>.</param>
        public void InsertRange(IEnumerable<T> entities)
        {
            dbSet.AddRange(entities);
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/>.</param>
        public void Update(T entity)
        {
            dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// The Delete.
        /// </summary>
        /// <param name="entity">The id<see cref="T"/>.</param>
        public void Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// Deletes range wise data
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool DeleteRange(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = this.dbSet;
            var elems = query.Where(filter);
            if (elems == null) return false;
            try
            {
                foreach (var entity in elems)
                {
                    Delete(entity);
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// The UpdateRange.
        /// </summary>
        /// <param name="entities">The entity<see cref="T"/>.</param>
        public void UpdateRange(IEnumerable<T> entities)
        {
            dbSet.AttachRange(entities);
            _dbContext.SaveChanges();
        }
    }
}
