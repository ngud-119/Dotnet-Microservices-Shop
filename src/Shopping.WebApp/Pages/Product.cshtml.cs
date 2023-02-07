using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.WebApp.Models;
using Shopping.WebApp.Services;

namespace Shopping.WebApp.Pages;

public class ProductModel : PageModel
{
    private readonly ICatalogService catalogService;
    private readonly IBasketService basketService;

    public ProductModel(ICatalogService catalogService, IBasketService basketService)
    {
        this.catalogService = catalogService;
        this.basketService = basketService;
    }

    public IEnumerable<string> CategoryList { get; set; } = new List<string>();
    public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();


    [BindProperty(SupportsGet = true)]
    public string SelectedCategory { get; set; }

    public async Task<IActionResult> OnGetAsync(string categoryName)
    {
        var productList = await catalogService.GetCatalog();
        CategoryList = productList.Select(e => e.Category).Distinct();

        if (string.IsNullOrWhiteSpace(categoryName))
        {
            ProductList = ProductList.Where(e => e.Category == categoryName);
            SelectedCategory = categoryName;
        }
        else
        {
            ProductList = productList;
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
            Quantity = 1,
            Color = "Black"
        });

        var basketUpdated = await basketService.UpdateBasket(basket);
        return RedirectToPage("Cart");
    }
}
