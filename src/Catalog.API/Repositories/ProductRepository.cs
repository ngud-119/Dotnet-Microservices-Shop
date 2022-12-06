using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext dbDontext;

    public ProductRepository(ICatalogContext dbDontext)
    {
        this.dbDontext = dbDontext ?? throw new ArgumentNullException(nameof(dbDontext));
    }

    public async Task Add(Product entity)
    {
        await this.dbDontext.Products.InsertOneAsync(entity);
    }

    public async Task<bool> Delete(string id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(e => e.Id, id);

        DeleteResult deleteResult = await this.dbDontext.Products.DeleteOneAsync(filter);
        
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await this.dbDontext.Products.Find(e => true).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByCategory(string categoryName)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(e => e.Category, categoryName);
        return await this.dbDontext.Products.Find(filter).ToListAsync();
    }

    public async Task<Product> GetById(string id)
    {
        return await this.dbDontext.Products.Find(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetByName(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(e => e.Name, name);
        return await this.dbDontext.Products.Find(filter).ToListAsync();
    }

    public async Task<bool> Update(Product entity)
    {
        var updateResult = await this.dbDontext.Products
            .ReplaceOneAsync(filter: e => e.Id == entity.Id, replacement: entity);
        
        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }
}
