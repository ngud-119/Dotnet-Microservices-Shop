using Ordering.Application.Common.Models;

namespace Ordering.Application.Common.Contracts.Notifications;

public interface IEmailService
{
    Task<bool> SendEmail(Email email);
}
