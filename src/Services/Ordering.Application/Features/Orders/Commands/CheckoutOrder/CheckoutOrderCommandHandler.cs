using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IMapper mapper;
    private readonly IOrderRepository orderRepository;
    private readonly IEmailService emailService;
    private readonly ILogger<CheckoutOrderCommandHandler> logger;

    public CheckoutOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
    {
        this.mapper = mapper;
        this.orderRepository = orderRepository;
        this.emailService = emailService;
        this.logger = logger;
    }

    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = mapper.Map<Order>(request);
        var newOrder = await orderRepository.AddAsync(orderEntity);

        logger.LogInformation($"Order {newOrder.Id} is successfully created.");

        var email = new Email()
        {
            To = "test@gmail.com",
            Subject = $"Your Order",
            Body = $"Order was created."
        };

        try
        {
            await emailService.SendEmail(email);
        }
        catch (Exception e)
        {
            logger.LogError($"Order {newOrder.Id} failed due to an error with the mail service: {e.Message}");
        }

        return newOrder.Id;
    }
}
