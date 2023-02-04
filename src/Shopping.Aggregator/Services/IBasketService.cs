using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public interface IBasketService
{
    Task<BasketModel> GetBasket(string username);
}
