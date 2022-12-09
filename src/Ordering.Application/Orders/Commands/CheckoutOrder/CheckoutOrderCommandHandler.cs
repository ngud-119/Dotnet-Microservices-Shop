using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Common.Contracts.Notifications;
using Ordering.Application.Common.Contracts.Persistence;
using Ordering.Application.Common.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Orders.Commands.CheckoutOrder;

public record CheckoutOrderCommand : IRequest<int>
{
    public string Username { get; set; } = "";
    public decimal TotalPrice { get; set; }

    // Billing Address
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string EmailAddress { get; set; } = "";
    public string AddressLine { get; set; } = "";
    public string Country { get; set; } = "";
    public string State { get; set; } = "";
    public string ZipCode { get; set; } = "";

    // Payment
    public string CardName { get; set; } = "";
    public string CardNumber { get; set; } = "";
    public string Expiration { get; set; } = "";
    public string CVV { get; set; } = "";
    public int PaymentMethod { get; set; }
}

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
