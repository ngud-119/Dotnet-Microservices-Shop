using Catalog.API.Entities;

namespace Catalog.API.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product> GetById(string id);
    Task<IEnumerable<Product>> GetByName(string name);
    Task<IEnumerable<Product>> GetByCategory(string categoryName);

    Task Add(Product entity);
    Task<bool> Update(Product entity);
    Task<bool> Delete(string id);
}
