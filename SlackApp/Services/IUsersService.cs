using System.Threading.Tasks;

namespace SlackApp.Services
{
    public interface IUsersService
    {
        Task<bool> SetPresence(string presence, string accessToken);
        Task<bool> SetStatus(string status, string accessToken);
    }
}