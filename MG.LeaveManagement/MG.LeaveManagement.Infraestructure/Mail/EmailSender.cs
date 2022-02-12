using MG.LeaveManagement.Application.Contracts.Infraestructure;
using MG.LeaveManagement.Application.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Infraestructure.Mail
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;


        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }


        public async Task<bool> SendEmail(Email email)
        {
            var client =new SendGridClient(_emailSettings.ApiKey);
            var to = new EmailAddress(email.To);
            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddres,
                Name = _emailSettings.FronName
            };

            var message = MailHelper.CreateSingleEmail(from, to, email.Subject,email.Body,email.Body);

            var response = await client.SendEmailAsync(message);

            return response.StatusCode == System.Net.HttpStatusCode.OK ||
                   response.StatusCode == System.Net.HttpStatusCode.Accepted;


        }
    }
}
