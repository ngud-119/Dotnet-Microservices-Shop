using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountRepository repository;

    public DiscountController(IDiscountRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet("{productName}", Name = "GetDiscount")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Coupon>> GetDiscount(string productName)
    {
        var coupon = await repository.GetDiscount(productName);
        return Ok(coupon);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
    {
        await repository.CreateDiscount(coupon);
        return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
    {
        return Ok(await repository.UpdateDiscount(coupon));
    }

    [HttpDelete("{productName}", Name = "DeleteDiscount")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Coupon>> DeleteDiscount(string productName)
    {
        return Ok(await repository.DeelteDiscount(productName));
    }
}
