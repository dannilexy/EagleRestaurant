
using Eaglez.Services.OrderAPI.messages;

namespace Eaglez.Services.Emails.Repository
{
    public interface IEmailRepository
    {
        Task SendAndLogEmail(UpdatePaymentResultMessage resultMessage);
    }
}
