using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IMapper mapper;
    private readonly IOrderRepository orderRepository;
    private readonly ILogger<UpdateOrderCommandHandler> logger;

    public UpdateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, ILogger<UpdateOrderCommandHandler> logger)
    {
        this.mapper = mapper;
        this.orderRepository = orderRepository;
        this.logger = logger;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToUpdate = await orderRepository.GetByIdAsync(request.Id);
        if (orderToUpdate == null)
        {
            logger.LogError("Order doen't exist in the database.");
            // throw new NotFoundException(nameof(Order), request.Id);
        }

        mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));
        await orderRepository.UpdateAsync(orderToUpdate);
        logger.LogError($"Order {orderToUpdate.Id} is successfully updated.");

        return Unit.Value;
    }
}
