using Shopping.WebApp.Models;

namespace Shopping.WebApp.Services;

public interface IBasketInterface
{
    Task<BasketModel> GetBasket(string username);
    Task<BasketModel> UpdateBasket(BasketModel model);
    Task CheckoutBasket(BasketCheckoutModel model);
}
