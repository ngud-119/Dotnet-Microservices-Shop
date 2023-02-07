using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.WebApp.Models;
using Shopping.WebApp.Services;

namespace Shopping.WebApp.Pages;

public class CheckOutModel : PageModel
{
    private readonly IBasketService basketService;
    private readonly IOrderService orderService;

    public CheckOutModel(IBasketService basketService, IOrderService orderService)
    {
        this.basketService = basketService;
        this.orderService = orderService;
    }

    [BindProperty]
    public BasketCheckoutModel Order { get; set; }

    public BasketModel Cart { get; set; } = new BasketModel();

    public async Task<IActionResult> OnGetAsync()
    {
        var userName = "1234";
        Cart = await basketService.GetBasket(userName);
        return Page();
    }

    public async Task<IActionResult> OnPostCheckOutAsync()
    {
        var userName = "1234";
        Cart = await basketService.GetBasket(userName);

        if (!ModelState.IsValid)
        {
            return Page();
        }

        Order.Username = userName;
        Order.TotalPrice = Cart.TotalPrice;

        await basketService.CheckoutBasket(Order);

        return RedirectToPage("Confirmation", "OrderSubmitted");
    }
}
