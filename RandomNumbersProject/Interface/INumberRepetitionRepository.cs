using Domain;
using Entities;

namespace Interface
{
    public interface INumberRepetitionRepository
    {
        /// <summary>
        /// Get item
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<NumberRepetition>> GetAsync();

        /// <summary>
        /// Add new item
        /// </summary>
        /// <param name="item"></param>
        Task AddAsync(NumberRepetition item);

        /// <summary>
        /// Addlist of item
        /// </summary>
        /// <param name="itemsList"></param>
        Task AddRangeAsync(List<NumberRepetition> itemsList);

        /// <summary>
        /// Gets number repetitions by number
        /// </summary>
        /// <returns></returns>
        Task<int> GetNumberRepetitionByNumber(int number);

        /// <summary>
        /// Gets number by number repetitions
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        Task<List<int>> GetNumberByNumberRepetitions(int repetition);

        /// <summary>
        /// Gets numbers with top repetitions
        /// </summary>
        /// <param name="repetitionNumber"></param>
        /// <returns></returns>
        Task<List<int>> GetNumbersWithTopRepetitions(int topCount);
    }
}