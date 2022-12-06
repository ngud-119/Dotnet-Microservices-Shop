using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IMapper mapper;
    private readonly IOrderRepository orderRepository;
    private readonly ILogger<DeleteOrderCommandHandler> logger;
    
    public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderCommandHandler> logger)
    {
        this.mapper = mapper;
        this.orderRepository = orderRepository;
        this.logger = logger;
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToDelete = await orderRepository.GetByIdAsync(request.Id);

        if (orderToDelete == null)
        {
            throw new NotFoundException(nameof(Order), request.Id);
        }

        await orderRepository.DeleteAsync(orderToDelete);
        logger.LogInformation($"Order {orderToDelete.Id} is successfully deleted.");

        return Unit.Value;
    }
}
