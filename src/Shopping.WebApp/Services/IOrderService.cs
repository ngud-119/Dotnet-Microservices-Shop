using Shopping.WebApp.Models;

namespace Shopping.WebApp.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
}
