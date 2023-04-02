
using System.Collections.Generic;

namespace Inreface
{
    public interface IRandomCounterRepository
    {
        /// <summary>
        /// Count the Numbers
        /// </summary>
        /// <param name="numbersList"></param>
        /// <returns></returns>
        Dictionary<int, int> GetCountNumbers(List<int> numbersList);
    }
}