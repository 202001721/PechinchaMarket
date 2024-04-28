using SendGrid.Helpers.Mail;
using SendGrid;
using PechinchaMarket.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace PechinchaMarket.Services
{
    public class EmailSender
    {
        private readonly IConfiguration _config; 
        public EmailSender(IConfiguration config = null)
        {
            _config = config;
        }
        public async Task SendEmail(string subject, string toEmail, string username,string message,DBPechinchaMarketContext _context)
        {
            var apiKey = _context.ApiKey.FirstOrDefault().Valor.ToString();
               
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
