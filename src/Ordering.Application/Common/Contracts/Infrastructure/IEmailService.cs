using Ordering.Application.Common.Models;

namespace Ordering.Application.Common.Contracts.Infrastructure;

public interface IEmailService
{
    Task<bool> SendEmail(Email email);
}
