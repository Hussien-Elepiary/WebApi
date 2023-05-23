using ECommerce_Demo_Core.Entities;
using ECommerce_Demo_Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Demo_Core.UnitOfWork
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> Complete();
    }
}
