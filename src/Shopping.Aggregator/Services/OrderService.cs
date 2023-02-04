using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;

public class OrderService : IOrderService
{
    public Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
    {
        throw new NotImplementedException();
    }
}
