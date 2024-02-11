using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Pc_Market_Service.Configuration;
using Microsoft.Extensions.Options;

namespace Pc_Market_Service.Email
{
    public class SendingEmails
    {
        private readonly EmailConfig _config;
        private readonly Logger.Logger _log;
        private string topic = "Informacja o Płatnościach";

        public SendingEmails(IOptions<EmailConfig> config,Logger.Logger log)
        {
            _config = config.Value;
            _log = log;
        }
        public void SendEmail(string contents, string recipientAddress,string customerName)
        {
            string recipentSender = _config.Email;
            string passwordSender = _config.EmailPassword;
            string smtpSender = _config.SmptSender;

            if (string.IsNullOrEmpty(recipientAddress))
            {
                _log.LogError($"Brak emaila w bazie danych dla kontrahenta : {customerName}");
                return;
            }
            try
            {
                MailMessage message = new MailMessage();

                message.From = new MailAddress(recipentSender);

                message.To.Add(new MailAddress(recipientAddress));

                message.Subject = topic;

                message.Body = contents;

                SmtpClient smtp = new SmtpClient(smtpSender);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(recipentSender, passwordSender);
                smtp.EnableSsl = true;
                smtp.Port = 587;

                smtp.Send(message);

                _log.LogInformation($"Email został wysłany do : {recipientAddress}");
            }
            catch (Exception ex)
            {
                _log.LogError($"Błąd : {ex.Message}");
            }
        }
    }
}
