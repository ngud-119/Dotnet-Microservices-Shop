using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    // To set the column name inside the db:
    [BsonElement("Name")]
    public string Name { get; set; }

    public string Category { get; set; }

    public string Summary { get; set; }

    public string Description { get; set; }

    public string ImageFile { get; set; }

    public decimal Price { get; set; }
}
