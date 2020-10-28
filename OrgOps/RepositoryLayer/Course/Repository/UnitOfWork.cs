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
        /// <param name="siteEnrichmentContext">The SiteEnrichmentContext<see cref="SiteEnrichmentContext"/>.</param>
        public UnitOfWork(RationCardContext siteEnrichmentContext)
        {
            if (context == null)
            {
                context = siteEnrichmentContext;
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