using ECommerce_Demo_Core.Entities.Cart;
using ECommerce_Demo_Core.IRepositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce_Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _connection;

        public BasketRepository(IConnectionMultiplexer connection)
        {
            _connection = connection.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _connection.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket = await _connection.StringGetAsync(basketId);
            return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UdateBasketAsync(CustomerBasket basket)
        {
            var createdOrUpdated = await _connection.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket),TimeSpan.FromDays(1));
            if (!createdOrUpdated) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
