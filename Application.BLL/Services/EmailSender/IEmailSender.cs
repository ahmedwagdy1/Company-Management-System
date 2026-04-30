using Application.DAL.Data.Models.Shared;

namespace Application.BLL.Services.EmailSender
{
    public interface IEmailSender
    {
        public void SendEmail(Email email);
    }
}
