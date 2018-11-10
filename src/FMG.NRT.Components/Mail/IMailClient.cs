using System;
using System.Threading.Tasks;

namespace FMG.NRT.Components.Mail
{
    public interface IMailClient
    {
        Task SendAsync(String email, String subject, String body);
    }
}
