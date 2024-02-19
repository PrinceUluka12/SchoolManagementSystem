using Identity.Models;

namespace Identity.Service.IService
{
    public interface IEmailService
    {
        void SendEmail(Notification notifications);
    }
}
