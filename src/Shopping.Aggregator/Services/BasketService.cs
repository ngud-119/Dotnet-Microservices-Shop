using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class BasketService : IBasketService
{
    private readonly HttpClient httpClient;

    public BasketService(HttpClient httpClient)
    {
        this.httpClient = httpClient;   
    }

    public async Task<BasketModel> GetBasket(string username)
    {
        var response = await httpClient.GetAsync($"/api/v1/Basket/{username}");
        return await response.ReadContentAs<BasketModel>();
    }
}
