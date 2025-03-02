using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Mail;

namespace WinFormsApp1
{
    internal class EmailService
    {

        private string smtpServer;
        private int smtpPort;
        private string smtpUsername;
        private string smtpPassword;

        public EmailService(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
        {
            this.smtpServer = smtpServer;
            this.smtpPort = smtpPort;
            this.smtpUsername = smtpUsername;
            this.smtpPassword = smtpPassword;
        }

        public void SendEmail(string fromEmail, string toEmail, string subject, string body, bool isHtml = true)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = true 
                };

                MailAddress from = new MailAddress(fromEmail);
                MailAddress to = new MailAddress(toEmail);

                MailMessage mailMessage = new MailMessage(from, to)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = isHtml,
                    SubjectEncoding = System.Text.Encoding.UTF8,
                    BodyEncoding = System.Text.Encoding.UTF8
                };

                smtpClient.Send(mailMessage);
                Console.WriteLine("Email sent successfully to " + toEmail);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("SmtpException: " + ex.Message);
                throw new ApplicationException("Failed to send email. Please check your SMTP configuration.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                throw new ApplicationException("An error occurred while sending email.");
            }
        }

    }
}
