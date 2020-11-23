using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ExamManagementSystem.Models.ServiceAccess
{
    public class MailServices
    {
        public static void SendEmail(string receiverEmail, string mailSubject, string mailBody)
        {
            List<string> receiverEmailList = new List<string>();
            receiverEmailList.Add(receiverEmail);
            SendEmail(receiverEmailList, mailSubject, mailBody);
        }

        public static void SendEmail(List<string> receiverEmailList, string mailSubject, string mailBody)
        {
            MailMessage mail = new MailMessage();
            string host = ConfigurationManager.AppSettings["SmtpHost"];
            string emailFrom = ConfigurationManager.AppSettings["emailFrom"];
            int port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            string credentialUsername = ConfigurationManager.AppSettings["SmtpUsername"];
            string credentialPassword = ConfigurationManager.AppSettings["SmtpPassword"];

            SmtpClient SmtpServer = new SmtpClient(host);

            mail.From = new MailAddress(emailFrom);
            foreach (string emailAddress in receiverEmailList)
            {
                mail.To.Add(emailAddress);
            }

            mail.Subject = mailSubject;
            mail.IsBodyHtml = true;
            mail.Body = mailBody;

            SmtpServer.Port = port;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential(credentialUsername, credentialPassword);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
