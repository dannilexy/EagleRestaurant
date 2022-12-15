using Eaglez.Services.Emails.Data;
using Eaglez.Services.Emails.Models;
using Eaglez.Services.Emails.Repository;
using Eaglez.Services.OrderAPI.messages;

namespace Eaglez.Services.Emails.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ApplicationDbContext _db;
        public EmailRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task SendAndLogEmail(UpdatePaymentResultMessage resultMessage)
        {
            //Implement an email sender or call other class library

            EmailLog log = new EmailLog
            {
                Email = resultMessage.Email,
                EmailSent = DateTime.Now,
                Log = $"Order - {resultMessage.OrderId} has been created successfully.",
            };
            _db.EmailLogs.Add(log);
            await _db.SaveChangesAsync();

        }
    }
}
