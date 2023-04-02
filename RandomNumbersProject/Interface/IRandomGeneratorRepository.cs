
using System.Collections.Generic;

namespace Inreface
{
    public interface IRandomGeneratorRepository
    {
        Task<List<int>> GenerateNumbers(int minNumber, int maxNumber, int numbersCount);
    }
}