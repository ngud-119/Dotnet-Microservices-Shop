using System.Net;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ILogger<CatalogController> logger;
    private readonly IProductRepository repository;
    
    public CatalogController(ILogger<CatalogController> logger, IProductRepository repository)
    {
        this.logger = logger;
        this.repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var list = await repository.GetAll();
        return Ok(list);
    }

    [HttpGet("{id:length(24)}", Name = "GetProduct")]
    [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<ActionResult<Product>> GetById(string id)
    {
        var entity = await repository.GetById(id);
        if (entity == null)
        {
            logger.LogError($"Product with id: {id}, not found.");
            return NotFound();
        }
        return Ok(entity);
    }

    [HttpGet]
    [Route("[action]/{category}", Name = "GetByCategory")]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetByCategory(string category)
    {
        var list = await repository.GetByCategory(category);
        return Ok(list);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<Product>> Add([FromBody] Product product)
    {
        await repository.Add(product);
        return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
    }

    [HttpPut]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<IActionResult> Update([FromBody] Product product)
    {
        return Ok(await repository.Update(product));
    }

    [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(Product), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteById(string id)
    {
        return Ok(await repository.Delete(id));
    }
}
