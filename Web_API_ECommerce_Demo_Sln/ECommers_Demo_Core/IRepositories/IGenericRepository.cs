using ECommerce_Demo_Core.Entities;
using ECommerce_Demo_Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Demo_Core.Repositories
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<IReadOnlyList<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetWithSpecAsync(ISpecification<T> spec);
		Task<int> GetCountWithSpec(ISpecification<T> spec);

		Task AddAsync(T entity);
		void Delete(T entity);
		void Update(T entity);

    }
}
