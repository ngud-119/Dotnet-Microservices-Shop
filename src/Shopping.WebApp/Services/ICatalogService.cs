using Shopping.WebApp.Models;

namespace Shopping.WebApp.Services;

public interface ICatalogService
{
    Task<IEnumerable<CatalogModel>> GetGatalog();
    Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);
    Task<CatalogModel> GetCatalog(string id);
    Task<CatalogModel> CreateCatalog(CatalogModel model);
}
