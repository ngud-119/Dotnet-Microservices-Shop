using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Common.Contracts.Notifications;
using Ordering.Application.Common.Contracts.Persistence;
using Ordering.Application.Common.Models;
using Ordering.Infrastructure.Notifications;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString"))
        );

        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
