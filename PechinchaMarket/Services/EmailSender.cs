using SendGrid.Helpers.Mail;
using SendGrid;
using PechinchaMarket.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace PechinchaMarket.Services
{
    public class EmailSender
    {
        private DBPechinchaMarketContext _context;
        public EmailSender(DBPechinchaMarketContext context) {
            _context = context;
        }
        public async Task SendEmail(string subject, string toEmail, string username,string message )
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("pechinchamarket@outlook.com", "PechinchaMarket");

           
           

            var to = new EmailAddress(toEmail,username);
            var plainTextContent = message;
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
