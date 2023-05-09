using ECommerce_Demo_Core.Entities;
using ECommerce_Demo_Core.Repositories;
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
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			//return await _dbContext.Set<T>().Where(X => X.Id == id).FirstOrDefaultAsync();
			return await _dbContext.Set<T>().FindAsync(id);
		}
	}
}
