using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Alllive.Helpers
{
    public class Email
    {
        public static string FormatBody(string template, Dictionary<string, string> data)
        {
            string result = template;
            foreach (string key in data.Keys)
            {
                result = result.Replace("{{" + key + "}}", data[key]);
            }
            return result;
        }

        public static MailMessage GetEmailMessage(string toEmail, string toName, string subject, string body)
        {
            var fromAddress = new MailAddress(ConfigurationManager.AppSettings["SMTPFromEmail"] ?? "", ConfigurationManager.AppSettings["SMTPFromName"] ?? "Automated Email");
            var toAddress = new MailAddress(toEmail, toName);
            var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body, IsBodyHtml = true };
            return message;
        }

        public static void SendMessage(MailMessage message)
        {
            using (var smtp = new SmtpClient(ConfigurationManager.AppSettings["SMTPHost"], 587))
            {
                smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUserName"], ConfigurationManager.AppSettings["SMTPPassword"]);
                smtp.Send(message);
            }
        }
      

    }
}