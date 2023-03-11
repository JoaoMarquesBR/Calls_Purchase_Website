using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Diagnostics;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGrid.Helpers.Mail.Model;

namespace BACKEND_MNG
{
    public class EmailSender
    {
      
        private const string apiKey = "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";
        private readonly SendGridClient client;



        static void Main(string[] args)
        {
            Console.WriteLine("Hi");
        }

        public async Task sendEmail(string emailReceiver,string sender ,string receiverName,string body,string title)
        {
            // Set up the SendGrid client and message details
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("johnmarques.br2002@gmail.com",sender);
            var to = new EmailAddress(emailReceiver, receiverName);
            var subject = title;
            var plainTextContent = "plainText?";
            var htmlContent =body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            // Send the email using the SendGrid client
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);

            Console.WriteLine(response.ToString());


        }

    }
}
