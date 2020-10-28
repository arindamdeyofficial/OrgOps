using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    /// <summary>
    /// Defines the <see cref="IUnitOfWork" />.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// The GetRepository.
        /// </summary>
        /// <typeparam name="TEntity">.</typeparam>
        /// <returns>The <see cref="IDbRepository{TEntity}"/>.</returns>
        IDbRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;

        /// <summary>
        /// The CompleteAsync.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        Task<int> CompleteAsync();
    }
}
