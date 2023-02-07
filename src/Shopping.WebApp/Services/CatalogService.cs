using Shopping.WebApp.Extensions;
using Shopping.WebApp.Models;

namespace Shopping.WebApp.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient httpClient;

    public CatalogService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<CatalogModel> CreateCatalog(CatalogModel model)
    {
        var response = await httpClient.PostAsJson($"/Catalog", model);

        if (response.IsSuccessStatusCode)
        {
            return await response.ReadContentAs<CatalogModel>();
        }
        else
        {
            throw new Exception("Something went wrong when calling the api.");
        }
    }

    public async Task<CatalogModel> GetCatalog(string id)
    {
        var response = await httpClient.GetAsync($"/Catalog/{id}");
        return await response.ReadContentAs<CatalogModel>();
    }

    public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
    {
        var response = await httpClient.GetAsync($"/Catalog/GetProductByCategory/{category}");
        return await response.ReadContentAs<List<CatalogModel>>();
    }

    public async Task<IEnumerable<CatalogModel>> GetGatalog()
    {
        var response = await httpClient.GetAsync("/Catalog");
        return await response.ReadContentAs<List<CatalogModel>>();
    }
}
