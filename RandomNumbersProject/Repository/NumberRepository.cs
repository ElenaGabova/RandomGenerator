
using AutoMapper;
using Database.Interface;
using Domain;
using Entities;
using FastMember;
using Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Repository.Properties;
using System.Collections.Generic;
using System.Data;
using static Constants.Constants;
namespace Repository
{
    public class NumberRepository:INumberRepository
    {
        private readonly IGenericRepository<NumberEntity> _numberBase;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private string connectionString = "";
        public NumberRepository(IGenericRepository<NumberEntity> numberBase, 
                                IMapper mapper, 
                                ILogger<NumberRepository> logger,
                                IConfiguration configuration)
        {
            _numberBase = numberBase;
            _mapper = mapper;
            _logger = logger;

            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Number>> GetAsync()
        {
            var numbersEntityList = await _numberBase.GetAsync();
            return _mapper.Map<IEnumerable<Number>>(numbersEntityList);
        }
  
        public async Task AddAsync(Number number)
        {
            var itemEntity = _mapper.Map<NumberEntity>(number);
            await _numberBase.AddAsync(itemEntity);
        }


        /// <summary>
        /// Add range data to database 
        /// </summary>
        /// <param name="itemsList"></param>
        /// <returns></returns>
        public async Task AddRangeAsync(List<Number> itemsList)
        {
            await _numberBase.TruncateAsync();
            var itemEntityList = _mapper.Map<List<NumberEntity>>(itemsList);

            {
                using (IDataReader reader = ObjectReader.Create(itemEntityList))
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlBulkCopy bcp = new SqlBulkCopy(connection))
                {
                    connection.Open();
                    int step = NumbersCount / 100;
                    bcp.DestinationTableName = "[Numbers]";
                    bcp.ColumnMappings.Add("Number", "Number");
                    bcp.EnableStreaming = true;
                    bcp.BatchSize = step;
                    bcp.NotifyAfter = step;

                    for (int i = 1; i <= NumbersCount / step; i++)
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
    }
}