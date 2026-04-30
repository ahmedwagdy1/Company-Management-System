using Application.DAL.Data.Models.Shared;
using System.Net;
using System.Net.Mail;

namespace Application.BLL.Services.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(Email email)
        {
            // Protocol => ftp, http
            // Email => Protocol => SMTP    
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("MariamShindyRoute@gmail.com", "auzooxcygjgcxauf");
            client.Send("MariamShindyRoute@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
