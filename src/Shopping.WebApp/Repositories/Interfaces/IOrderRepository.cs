using Shopping.WebApp.Entities;

namespace Shopping.WebApp.Repositories;

public interface IOrderRepository
{
    Task<Order> CheckOut(Order order);
    Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
}
