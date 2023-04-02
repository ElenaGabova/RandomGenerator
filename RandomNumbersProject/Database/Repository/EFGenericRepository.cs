using Database;
using Database.Interface;
using Database.Properties;
using Entities;
using FastMember;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Linq.Expressions;

namespace Database.Repository
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        DatabaseContext _context;
        DbSet<TEntity> _dbSet;
        ILogger _logger;
        private bool isContextChanged
        {
            set
            {
                if (value == true)
                {
                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(string.Format(Resources.DatabaseSaveChangesError, ex.Message));
                    }
                }
            }
        }

        public EFGenericRepository(DatabaseContext context, ILogger<EFGenericRepository<TEntity>> logger)
        {
            isContextChanged = false;
            _context = context;
            _dbSet = context.Set<TEntity>();
            _logger = logger;
        }

        /// <summary>
        /// Truncate table method
        /// </summary>
        /// <returns></returns>
        public async Task TruncateAsync()
        {
            if (_dbSet.EntityType.Name is "Entities.NumberEntity")
            {
                _context.Database.ExecuteSqlRaw("TRUNCATE TABLE Numbers");
                return;
            }  

            if (_dbSet.EntityType.Name is "Entities.NumberRepetitionEntity")
            {
                _context.Database.ExecuteSqlRaw("TRUNCATE TABLE NumberRepetitions");
                return;
            }

            var entityList = await GetAsync();
            _dbSet.RemoveRange(entityList);
            isContextChanged = true;
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(TEntity item)
        {
             await _dbSet.AddAsync(item);
            isContextChanged = true;
        }

        public async Task AddRangeAsync(List<TEntity> itemsList)
        { 
              await _dbSet.AddRangeAsync(itemsList);
             isContextChanged = true;
        }

        public async Task <IQueryable<TEntity>> GetAsyncWithWhere(Expression<Func<TEntity, Boolean>> where)
        {
            IQueryable<TEntity> query = _dbSet;
            return query.Where(where);
        }
    }
}