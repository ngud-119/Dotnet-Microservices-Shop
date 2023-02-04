using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class BasketService : IBasketService
{
    public Task<BasketModel> GetBasket(string username)
    {
        throw new NotImplementedException();
    }
}
