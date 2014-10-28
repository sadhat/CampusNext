using System.Net;
using System.Net.Mail;
using SendGrid;
namespace CampusNext.Communication
{
    public interface IEmailSender
    {
        void Send(string from, string[] recipients, string subject, string messageBody, bool isHtml = true);
    }

    public class EmailSender : IEmailSender
    {
        public void Send(string from, string[] recipients, string subject, string messageBody, bool isHtml = true)
        {
            var message = new SendGridMessage {From = new MailAddress(@from)};

            message.AddTo(recipients);

            message.Subject = subject;

            if (isHtml)
            {
                message.Html = messageBody;
            }
            else
            {
                message.Text = messageBody;
            }

            const string username = "sadhat";
            const string pswd = "monamoni2007";

            var credentials = new NetworkCredential(username, pswd);

            // Create an Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            transportWeb.Deliver(message);
        }
    }
}
