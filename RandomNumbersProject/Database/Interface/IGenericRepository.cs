using System.Linq.Expressions;

namespace Database.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {

        /// <summary>
        /// Get item
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync();

        /// <summary>
        /// Get item with where
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetAsyncWithWhere(Expression<Func<TEntity, Boolean>> where);

        /// <summary>
        /// Add new item
        /// </summary>
        /// <param name="item"></param>
        Task AddAsync(TEntity item);

        /// <summary>
        /// Addlist of item
        /// </summary>
        /// <param name="itemsList"></param>
        Task AddRangeAsync(List<TEntity> itemsList);
        Task TruncateAsync();
    }
}