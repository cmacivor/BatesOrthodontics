using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace BatesOrtho
{
    public class SendGmail
    {

        public static string GmailHost { get; set; }
        public static int GmailPort { get; set; }
        public static bool GmailSSL { get; set; }

        private string fromEmail = System.Configuration.ConfigurationManager.AppSettings["FromAddress"];

        public string FromEmail
        {
            get { return fromEmail;}
        }
        private string toEmail = System.Configuration.ConfigurationManager.AppSettings["ToAddress"];

        public string ToEmail
        {
            get { return toEmail; }
        }
        private string gmailPassword = "Caesar2810$";

        public string GmailPassword
        {
            get { return gmailPassword; }
        }
        private string gmailUserName = "cmacivor82@gmail.com";

        public string GmailUserName
        {
            get { return gmailUserName; }
        }
        private bool isHtml = true;

        public bool IsHtml 
        {
            get { return isHtml; }
        }
        

        public string Subject { get; set; }
        public string Body { get; set; }
        //public bool IsHtml { get; set; }

        static SendGmail()
        {
            GmailHost = "smtp.gmail.com";
            GmailPort = 587; // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
            GmailSSL = true;
        }

        public void Send(string subjectText, string messageBody)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = GmailHost;
            smtp.Port = GmailPort;
            smtp.EnableSsl = GmailSSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(GmailUserName, GmailPassword);
            smtp.Timeout = 30000;
            
            using (var message = new MailMessage("cmacivor@yahoo.com", ToEmail, subjectText, messageBody))
            {
                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = IsHtml;
                smtp.Send(message);
            }
        }
    }
}