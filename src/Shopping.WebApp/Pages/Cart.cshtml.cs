using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.WebApp.Models;
using Shopping.WebApp.Services;

namespace Shopping.WebApp;

public class CartModel : PageModel
{
    private readonly IBasketService _basketService;

    public CartModel(IBasketService basketService)
    {
        _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
    }

    public BasketModel Cart { get; set; } = new BasketModel();

    public async Task<IActionResult> OnGetAsync()
    {
        var userName = "swn";
        Cart = await _basketService.GetBasket(userName);

        return Page();
    }

    public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
    {
        var userName = "swn";
        var basket = await _basketService.GetBasket(userName);

        var item = basket.Items.Single(x => x.ProductId == productId);
        basket.Items.Remove(item);

        var basketUpdated = await _basketService.UpdateBasket(basket);

        return RedirectToPage();
    }
}
