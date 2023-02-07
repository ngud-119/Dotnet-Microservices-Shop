using Shopping.WebApp.Models;

namespace Shopping.WebApp.Services;

public interface IBasketService
{
    Task<BasketModel> GetBasket(string username);
    Task<BasketModel> UpdateBasket(BasketModel model);
    Task CheckoutBasket(BasketCheckoutModel model);
}
