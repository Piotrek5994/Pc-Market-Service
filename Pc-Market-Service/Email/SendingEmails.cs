using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Pc_Market_Service.Email
{
    public class SendingEmails
    {
        private readonly Logger.Logger _log;
        private string topic = "Informacja o Płatnościach";

        public SendingEmails(Logger.Logger log)
        {
            _log = log;
        }
        public string SendEmail(string contents, string recipientAddress)
        {
            string recipentSender = "piotrek5994@gmail.com";
            string passwordSender = "neyy xmvp qmls sakk";
            string smtpSender = "smtp.gmail.com";
            if(string.IsNullOrEmpty(recipientAddress))
            {
                _log.LogError($"Brak emaila w bazie danych");
                return "Brak emaila w bazie danych";
            }
            try
            {
                MailMessage message = new MailMessage();

                message.From = new MailAddress(recipentSender);

                message.To.Add(new MailAddress("piotrekd@elte-s.eu"));

                message.Subject = topic;

                message.Body = contents;

                SmtpClient smtp = new SmtpClient(smtpSender);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(recipentSender, passwordSender);
                smtp.EnableSsl = true;
                smtp.Port = 587;

                smtp.Send(message);

                _log.LogInformation($"Email został wysłany do : {recipientAddress}");
                return "OK";
            }
            catch (Exception ex)
            {
                _log.LogError($"Błąd : {ex.Message}");
                return ex.ToString();
            }
        }
    }
}
