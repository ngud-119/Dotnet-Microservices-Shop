using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.WebApp.Models;
using Shopping.WebApp.Services;

namespace Shopping.WebApp.Pages;

public class ProductDetailModel : PageModel
{
    private readonly ICatalogService catalogService;
    private readonly IBasketService basketService;

    public ProductDetailModel(ICatalogService catalogService, IBasketService basketService)
    {
        this.catalogService = catalogService;
        this.basketService = basketService;
    }

    public CatalogModel Product { get; set; }

    [BindProperty]
    public string Color { get; set; }

    [BindProperty]
    public int Quantity { get; set; }

    public async Task<IActionResult> OnGetAsync(string productId)
    {
        if (productId == null)
        {
            return NotFound();
        }

        Product = await catalogService.GetCatalog(productId);
        if (Product == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(string productId)
    {
        var product = await catalogService.GetCatalog(productId);

        var userName = "123";
        var basket = await basketService.GetBasket(userName);

        basket.Items.Add(new BasketItemModel
        {
            ProductId = productId,
            ProductName = product.Name,
            Price = product.Price,
            Quantity = Quantity,
            Color = Color
        });

        var basketUpdated = await basketService.UpdateBasket(basket);
        return RedirectToPage("Cart");
    }
}
