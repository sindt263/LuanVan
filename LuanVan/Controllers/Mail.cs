using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace LuanVan.Controllers
{
    public class Mail
    {
        public string SendMailFull(string mailFrom, string mailpass, string host, string port, string mailTo, string subject, string content, bool enableSsl)
        {
            try
            {
                var msg = new MailMessage
                {
                    IsBodyHtml = true,
                    Body = content,
                    From = new MailAddress(mailFrom, mailFrom)
                };
                msg.To.Add(new MailAddress(mailTo));
                msg.Subject = subject;

                var client = new SmtpClient(host, int.Parse(port))
                {
                    Credentials =
                        new NetworkCredential(mailFrom, mailpass),
                    EnableSsl = enableSsl
                };

                client.Send(msg);

                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}