using Microsoft.EntityFrameworkCore;
using Ordering.Application.Common.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(OrderContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        var orderList = await this.dbContext.Orders
                        .Where(e => e.Username == userName)
                        .ToListAsync();

        return orderList;    
    }
}
