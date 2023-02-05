using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.WebApp.Repositories;

namespace Shopping.WebApp.Pages;

public class CartModel : PageModel
{
    private readonly ICartRepository _cartRepository;

    public CartModel(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
    }

    public Entities.Cart Cart { get; set; } = new Entities.Cart();

    public async Task<IActionResult> OnGetAsync()
    {
        Cart = await _cartRepository.GetCartByUserName("test");

        return Page();
    }

    public async Task<IActionResult> OnPostRemoveToCartAsync(int cartId, int cartItemId)
    {
        await _cartRepository.RemoveItem(cartId, cartItemId);
        return RedirectToPage();
    }
}
