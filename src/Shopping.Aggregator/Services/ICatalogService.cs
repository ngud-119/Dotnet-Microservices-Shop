using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public interface ICatalogService
{
    Task<IEnumerable<CatalogModel>> GetGatalog();
    Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);
    Task<CatalogModel> GetCatalog(string id);
}
