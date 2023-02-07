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
        var response = await httpClient.GetAsync("/api/v1/Catalog");
        return await response.ReadContentAs
    }

    public Task<CatalogModel> GetCatalog(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CatalogModel>> GetGatalog()
    {
        throw new NotImplementedException();
    }
}
