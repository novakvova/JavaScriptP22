using LohikaBackend.Models;

namespace LohikaBackend.Abastract
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
