using Shopping.WebApp.Models;

namespace Shopping.WebApp.Services;

public class BasketService : IBasketService
{
    public Task CheckoutBasket(BasketCheckoutModel model)
    {
        throw new NotImplementedException();
    }

    public Task<BasketModel> GetBasket(string username)
    {
        throw new NotImplementedException();
    }

    public Task<BasketModel> UpdateBasket(BasketModel model)
    {
        throw new NotImplementedException();
    }
}
