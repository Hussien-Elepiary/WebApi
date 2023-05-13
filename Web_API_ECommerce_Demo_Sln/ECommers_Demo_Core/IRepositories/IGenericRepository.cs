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
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetWithSpecAsync(ISpecification<T> spec);
    }
}
