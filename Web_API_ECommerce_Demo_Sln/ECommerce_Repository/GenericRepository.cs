using ECommerce_Demo_Core.Entities;
using ECommerce_Demo_Core.Repositories;
using ECommerce_Demo_Core.Specifications;
using ECommerce_Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _dbContext;

		public GenericRepository(StoreContext dbContext) // Ask Clr to create an object of the Context implictly
        {
			_dbContext = dbContext;
		}

		/// <summary>
		/// GetAllAsync dose not Get the related data will add a new design on it to get the related data 
		/// </summary>
		/// <returns></returns>
		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

        

        public async Task<T> GetByIdAsync(int id)
		{
			//return await _dbContext.Set<T>().Where(X => X.Id == id).FirstOrDefaultAsync();
			return await _dbContext.Set<T>().FindAsync(id);
		}

        #region Specification
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
		{
			return await ApplySpecification(spec).ToListAsync();
		}

		public async Task<T> GetWithSpecAsync(ISpecification<T> spec)
		{
			return await ApplySpecification(spec).FirstOrDefaultAsync();
		}

        public async Task<int> GetCountWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        /// <summary>
        /// calls SpecificationEvalutor<T> and sends the dbContext.set<T>
        /// </summary>
        /// <param name="spec">needs the spec list</param>
        /// <returns>the Query string</returns>
        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
		{
			return SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>(),spec);
		}

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }


        #endregion

    }
}
