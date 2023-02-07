using Shopping.WebApp.Models;

namespace Shopping.WebApp.Services;

public class OrderService : IOrderService
{
    public Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
    {
        throw new NotImplementedException();
    }
}
