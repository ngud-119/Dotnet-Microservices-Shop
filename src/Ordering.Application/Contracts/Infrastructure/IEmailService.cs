using Ordering.Application.Models;

namespace Ordering.Application.Contracts.Infrastructure;

public interface IEmailService
{
    Task<bool> SendEmail(Email email);
}
