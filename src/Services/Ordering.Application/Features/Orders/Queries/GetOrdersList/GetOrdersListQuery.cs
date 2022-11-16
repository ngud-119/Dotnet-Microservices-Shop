using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQuery : IRequest<List<OrdersVm>>
{
    public string Username { get; set; }

    public GetOrdersListQuery(string username)
    {
        this.Username = username;
    }
}
