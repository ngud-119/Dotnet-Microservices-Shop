using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shopping.WebApp.Models;
using Shopping.WebApp.Services;

namespace Shopping.WebApp.Pages;

public class OrderModel : PageModel
{
    private readonly IOrderService orderService;

    public OrderModel(IOrderService orderService)
    {
        this.orderService = orderService;
    }

    public IEnumerable<OrderResponseModel> Orders { get; set; } = new List<OrderResponseModel>();

    public async Task<IActionResult> OnGetAsync()
    {
        Orders = await orderService.GetOrdersByUserName("test");

        return Page();
    }
}
