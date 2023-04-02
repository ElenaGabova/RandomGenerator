
using System.Collections.Generic;

namespace Inreface
{
    public class NumberCounterRepository : IRandomCounterRepository
    {

        Dictionary<int, int> numbersCountDictonary;

        public NumberCounterRepository()
        {
            numbersCountDictonary = new Dictionary<int, int>();
        }

        /// <summary>
        /// Get numbers count from array
        /// Key - number
        /// Value - Repetition
        /// </summary>
        /// <param name="numbersList"></param>
        /// <returns></returns>
        public Dictionary<int, int> GetCountNumbers(List<int> numbersList)
        {

            foreach(var number in numbersList)
            {
                if (numbersCountDictonary.ContainsKey(number))
                    numbersCountDictonary[number]++;
                else
                    numbersCountDictonary.Add(number, 1);
            }
            return numbersCountDictonary;
        }
    }
}