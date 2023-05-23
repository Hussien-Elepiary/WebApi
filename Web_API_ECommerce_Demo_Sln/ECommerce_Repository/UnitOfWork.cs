using ECommerce_Demo_Core.Entities;
using ECommerce_Demo_Core.Repositories;
using ECommerce_Demo_Core.UnitOfWork;
using ECommerce_Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private Hashtable _repo;


        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repo == null)
                _repo = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repo.ContainsKey(type))
            {
                var repo = new GenericRepository<TEntity>(_dbContext);
                _repo.Add(type, repo);
            }

            return _repo[type] as IGenericRepository<TEntity>;

        }


        public async Task<int> Complete()
        {
           return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

        
    }
}
