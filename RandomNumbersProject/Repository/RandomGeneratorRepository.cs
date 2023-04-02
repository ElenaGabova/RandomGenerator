using randomGenerator;
using Inreface;
using static Constants.Constants;
namespace Interface;

public class RandomGeneratorRepository : IRandomGeneratorRepository
{
    private List<int> numbersArray;

    public async Task<List<int>> GenerateNumbers(int minNumber, int maxNumber, int numbersCount)
    {
        if (numbersArray== null || numbersArray.Count < numbersCount)
            numbersArray = new List<int>(numbersCount); 

        while(numbersArray.Count < numbersCount)
        {
            Parallel.For(minNumber, numbersCount, x =>
        
            {
                var value = ThreadSafeRandom.Next(maxNumber);
                if(numbersArray.Count < numbersCount)
                    numbersArray.Add(value);
            });

        }
        
        return numbersArray;
    }
}
