using preAceleracionDisney.Entities;
using System.Threading.Tasks;

namespace preAceleracionDisney.Interfaces
{
    public interface IMailService
    {
        Task SendEmail(User user);
    }
}