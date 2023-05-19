using ECommerce_Demo_Core.Entities.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Demo_Core.IRepositories
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);
        Task<CustomerBasket?> UdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId); 
    }
}
