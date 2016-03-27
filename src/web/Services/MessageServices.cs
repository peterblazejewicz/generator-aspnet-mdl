using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;

namespace WebApplication.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public AuthMessageSender(IOptions<AuthMessageSenderOptions> options)
        {
            this.Options = options.Value;
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            var msg = new SendGrid.SendGridMessage();
            msg.AddTo(email);
            msg.From = new MailAddress("joe@example.com", "Joe Material");
            msg.Subject = subject;
            msg.Text = message;
            msg.Html = message;
            var credentials = new NetworkCredential(
                Options.SendGridUser,
                Options.SendGridKey
            );
            Console.WriteLine(credentials);
            Console.WriteLine(credentials.UserName);
            Console.WriteLine(credentials.Password);
            var transport = new SendGrid.Web(credentials);
            if(transport != null) {
                Console.WriteLine("Sending");
                return transport.DeliverAsync(msg);
            } else {
                Console.WriteLine("Not sent");
                return Task.FromResult(0);
            }
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            var twilio = new Twilio.TwilioRestClient(
                Options.SID,
                Options.AuthToken);
            var result = twilio.SendMessage(Options.SendNumber, number, message);
            // Use the debug output for testing without receiving a SMS message.
            // Remove the Debug.WriteLine(message) line after debugging.
            System.Diagnostics.Debug.WriteLine(message);
            return Task.FromResult(0);
        }

        public AuthMessageSenderOptions Options { get; }
    }
}
