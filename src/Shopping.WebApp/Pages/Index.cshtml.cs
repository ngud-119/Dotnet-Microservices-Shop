using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.WebApp.Models;
using Shopping.WebApp.Services;

namespace Shopping.WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ICatalogService catalogService;
    private readonly IBasketService basketService;

    public IndexModel(ICatalogService catalogService, IBasketService basketService)
    {
        this.catalogService = catalogService;
        this.basketService = basketService;
    }

    public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();

    public async Task<IActionResult> OnGetAsync()
    {
        ProductList = await catalogService.GetCatalog();
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
            Quantity = 1,
            Color = "Black"
        });

        var basketUpdated = await basketService.UpdateBasket(basket);
        return RedirectToPage("Cart");
    }
}
