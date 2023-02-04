using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient httpClient;

    public CatalogService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
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
