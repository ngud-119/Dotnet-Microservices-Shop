using Shopping.WebApp.Extensions;
using Shopping.WebApp.Models;

namespace Shopping.WebApp.Services;

public class BasketService : IBasketService
{
    private readonly HttpClient httpClient;

    public BasketService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task CheckoutBasket(BasketCheckoutModel model)
    {
        var response = await httpClient.PostAsJson($"/Basket/Checkout", model);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Something went wrong when calling the api.");
        }
    }

    public async Task<BasketModel> GetBasket(string userName)
    {
        var response = await httpClient.GetAsync($"/Basket/{userName}");
        return await response.ReadContentAs<BasketModel>();
    }

    public async Task<BasketModel> UpdateBasket(BasketModel model)
    {
        var response = await httpClient.PostAsJson($"/Basket", model);
        if (response.IsSuccessStatusCode)
        {
            return await response.ReadContentAs<BasketModel>();
        }
        else
        {
            throw new Exception("Something went wrong when calling the api.");
        }
    }
}
