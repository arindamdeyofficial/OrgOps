using RepositoryLayer.Models;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    /// <summary>
    /// Defines the <see cref="UnitOfWork" />.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UnitOfWork : IUnitOfWork
        //where TContext : DbContext, IDisposable
    {
        /// <summary>
        /// Defines the context.
        /// </summary>
        private readonly RationCardContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="RationCardContext">The RationCardContext<see cref="RationCardContext"/>.</param>
        public UnitOfWork(RationCardContext rationCardContext)
        {
            if (context == null)
            {
                context = rationCardContext;
            }
        }

        /// <summary>
        /// The GetRepository.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns>The <see cref="IDbRepository{TEntity}"/>.</returns>
        public IDbRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            return new DbRepository<TEntity>(context);
        }

        /// <summary>
        /// The CompleteAsync.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public async Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}