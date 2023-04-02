
using AutoMapper;
using Database.Interface;
using Entities;
using FastMember;
using Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Repository.Properties;
using System.Data;
using static Constants.Constants;

namespace Repository
{
    public class NumberRepetitionRepository : INumberRepetitionRepository
    {
        private readonly IGenericRepository<NumberRepetitionEntity> _numberRepetitionBase;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public NumberRepetitionRepository(IGenericRepository<NumberRepetitionEntity> numberRepetitionBase, IMapper mapper, ILogger<NumberRepetitionRepository> logger)
        {
            _numberRepetitionBase = numberRepetitionBase;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<NumberRepetition>> GetAsync()
        {
            var numbersEntityList = await _numberRepetitionBase.GetAsync();
            return _mapper.Map<IEnumerable<NumberRepetition>>(numbersEntityList);
        }
  
        public async Task AddAsync(NumberRepetition number)
        {
            var itemEntity = _mapper.Map<NumberRepetitionEntity>(number);
            await _numberRepetitionBase.AddAsync(itemEntity);
        }

        /// <summary>
        /// Add range data to database 
        /// </summary>
        /// <param name="itemsList"></param>
        /// <returns></returns>
        public async Task AddRangeAsync(List<NumberRepetition> itemsList)
        {
            await _numberRepetitionBase.TruncateAsync();

            var itemEntityList = _mapper.Map<List<NumberRepetitionEntity>>(itemsList);
            {
                using (IDataReader reader = ObjectReader.Create(itemEntityList))
                using (SqlConnection connection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Database=RandomNumbers;Trusted_Connection=false;User Id =sa;Password=K6y&2xS1qa!"))
                using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                {
                    connection.Open();
                    bcp.DestinationTableName = "[NumberRepetitions]";
                    bcp.ColumnMappings.Add("Number", "Number");
                    bcp.ColumnMappings.Add("RepetitionAmount", "RepetitionAmount");
                    int step = NumbersCount / 100;
                    for (int i = 1; i <= itemEntityList.Count() / step; i++)
                    {
                        var smallItemEntityList = itemEntityList.Skip((i - 1) * step).Take(step).ToList();
                        IDataReader newReader = ObjectReader.Create(smallItemEntityList);

                        try 
                        {
                            await bcp.WriteToServerAsync(newReader);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(string.Format(Resources.DatabaseSaveChangesError, ex.Message));
                        }
                        
                    }
                }
            }
        }

        public async Task<int> GetNumberRepetitionByNumber(int number)
        {
            var numbersEntityList = await _numberRepetitionBase.GetAsyncWithWhere(n => n.Number == number);
            return numbersEntityList.Select(n => n.RepetitionAmount).FirstOrDefault();
        }

        public async Task<List<int>> GetNumberByNumberRepetitions(int repetition)
        {
            var numbersEntityList = await _numberRepetitionBase.GetAsyncWithWhere(n => n.RepetitionAmount == repetition);
            return numbersEntityList.Select(n => n.Number).ToList();
        }

        public async Task<List<int>> GetNumbersWithTopRepetitions(int topCount)
        {
            var numbersEntityList = await _numberRepetitionBase.GetAsync();
            numbersEntityList = numbersEntityList.OrderByDescending(n => n.RepetitionAmount);
            if (topCount > numbersEntityList.Count())
                return numbersEntityList.Select(n => n.Number).ToList();
            else
                return numbersEntityList.Take(topCount).Select(n => n.Number).ToList();
        }
    }
}