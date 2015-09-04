using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Logging;
using SynchronizationService.Properties;

namespace SynchronizationService.Infrastructure
{
    public static class EmailNotifier
    {
        private static SmtpClient mailClient;

        static EmailNotifier()
        {
            string emailServerHost = Settings.Default.EmailServerHost;
            string emailUsername = Settings.Default.EmailUsername;
            string emailPassword = Settings.Default.EmailPassword;

            mailClient = new SmtpClient(emailServerHost);
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.UseDefaultCredentials = false;
            mailClient.Credentials = new NetworkCredential(emailUsername, emailPassword);
        }

        public static void SendExceptionEmail(string subject, string body)
        {
            var message = new MailMessage(Settings.Default.EmailUsername, Settings.Default.EmailNotificationAddress);
            message.IsBodyHtml = false;
            message.Subject = subject;
            message.Body = body;
            message.Priority = MailPriority.High;

            mailClient.Send(message);
        }
    }
}
