using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

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

        private string gmailPassword = "batesortho";
        private string errorGmailPassword = "Caesar2810$";

        public string ErrorGmailPassword 
        {
            get { return errorGmailPassword; }
        }

        public string GmailPassword
        {
            get { return gmailPassword; }
        }
        private string gmailUserName = "batesorthodontics@gmail.com";
        private string errorGmailUserName = "cmacivor82@gmail.com";

        public string GmailUserName
        {
            get { return gmailUserName; }
        }

        public string ErrorGmailUserName 
        {
            get { return errorGmailUserName; } 
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

        //ideas adopted from here:
        //http://stackoverflow.com/questions/20129933/sendmailasync-use-in-mvc
        //public async Task Send(string subjectText, string messageBody)
        //{
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = GmailHost;
        //    smtp.Port = GmailPort;
        //    smtp.EnableSsl = GmailSSL;
        //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    smtp.UseDefaultCredentials = false;
        //    smtp.Credentials = new NetworkCredential(GmailUserName, GmailPassword);
        //    smtp.Timeout = 30000;
            
        //    using (var message = new MailMessage("cmacivor@yahoo.com", ToEmail, subjectText, messageBody))
        //    {
        //        message.Subject = Subject;
        //        message.Body = Body;
        //        message.IsBodyHtml = IsHtml;
        //        //smtp.Send(message);
        //        //smtp.SendAsync(message, null);
        //        await smtp.SendMailAsync(message);
        //    }
        //}

        public void Send(string subjectText, string messageBody)
        {
            
            SmtpClient smtp = new SmtpClient();
            smtp.Host = GmailHost;
            smtp.Port = GmailPort;
            smtp.EnableSsl = GmailSSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(ToEmail, GmailPassword);
            smtp.Timeout = 30000;


            MailMessage message = new MailMessage(ToEmail, ToEmail, subjectText, messageBody);
            message.Subject = Subject;
            message.Body = Body;
            message.IsBodyHtml = IsHtml;

            new Thread(() =>
            {
                try
                {
                    smtp.Send(message);
                    message.Dispose();
                }
                catch (SmtpException ex)
                {
                    this.SendError("unable to login", ex.Message + " " + ex.InnerException);
                }
                    
            }).Start();
           
        }

        public void SendError(string subjectText, string messageBody)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = GmailHost;
            smtp.Port = GmailPort;
            smtp.EnableSsl = GmailSSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(ErrorGmailUserName, ErrorGmailPassword);
            smtp.Timeout = 30000;


            MailMessage message = new MailMessage(ErrorGmailUserName, ErrorGmailUserName, subjectText, messageBody);
            message.Subject = Subject;
            message.Body = Body;
            message.IsBodyHtml = IsHtml;

            new Thread(() =>
            {
                smtp.Send(message);
                message.Dispose();
            }).Start();
        }
    }
}