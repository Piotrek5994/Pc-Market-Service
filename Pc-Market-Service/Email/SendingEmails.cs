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
        public string SendEmail(string topic, string contents, string recipientAddress)
        {
            string adresNadawcy = "piotrek5994@gmail.com";
            string hasloNadawcy = "neyy xmvp qmls sakk";
            string smtpNadawcy = "smtp.gmail.com";

            try
            {
                MailMessage message = new MailMessage();

                message.From = new MailAddress(adresNadawcy);

                message.To.Add(new MailAddress(recipientAddress));

                message.Subject = topic;
                message.Body = contents;

                SmtpClient smtp = new SmtpClient(smtpNadawcy);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(adresNadawcy, hasloNadawcy);
                smtp.EnableSsl = true; // Ustaw na true, jeśli wymagane jest SSL
                smtp.Port = 587; // Ustaw port SMTP

                smtp.Send(message);

                return "OK";
            }
            catch (Exception ex)
            {
                return "Błąd: " + ex.Message;
            }
        }
    }
}
