using Domain;
using Entities;

namespace Interface
{
    public interface INumberRepository
    {
        /// <summary>
        /// Get item
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Number>> GetAsync();

        /// <summary>
        /// Add new item
        /// </summary>
        /// <param name="item"></param>
        Task AddAsync(Number item);

        /// <summary>
        /// Addlist of item
        /// </summary>
        /// <param name="itemsList"></param>
        Task AddRangeAsync(List<Number> itemsList);
    }
}