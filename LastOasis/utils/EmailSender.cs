using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;
namespace LastOasis.utils
{
    public class EmailSender
    {
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@localhost.com", "Oasis User");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var bytes = File.ReadAllBytes("/files/authentication.txt");
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment("authentication.txt", file);

            var response = client.SendEmailAsync(msg);
        }
    }
}