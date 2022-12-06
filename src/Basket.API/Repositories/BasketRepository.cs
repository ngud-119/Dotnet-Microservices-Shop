using System.Text.Json;
using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache redisCache;

    public BasketRepository(IDistributedCache redisCache)
    {
        this.redisCache = redisCache;
    }

    public async Task DeleteBasket(string username)
    {
        await redisCache.RemoveAsync(username);
    }

    public async Task<ShoppingCart?> GetBasket(string username)
    {
        var basket = await redisCache.GetStringAsync(username);
        if (string.IsNullOrEmpty(basket))
        {
            return null;
        }
        return JsonSerializer.Deserialize<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart?> UpdateBasket(ShoppingCart basket)
    {
        await redisCache.SetStringAsync(basket.Username, JsonSerializer.Serialize(basket));
        return await GetBasket(basket.Username); 
    }
}
