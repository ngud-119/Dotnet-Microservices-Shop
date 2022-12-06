using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersVm>>
{
    private readonly IMapper mapper;
    private readonly IOrderRepository orderRepository;

    public GetOrdersListQueryHandler(IMapper mapper, IOrderRepository orderRepository)
    {
        this.mapper = mapper;
        this.orderRepository = orderRepository;
    }

    public async Task<List<OrdersVm>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
    {
        var orderList = await orderRepository.GetOrdersByUserName(request.Username);
        return mapper.Map<List<OrdersVm>>(orderList);
    }
}
