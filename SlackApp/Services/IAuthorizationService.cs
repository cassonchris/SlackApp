using System.Threading.Tasks;

namespace SlackApp.Services
{
    public interface IAuthorizationService
    {
        Task<bool> GrantAsync(string code);
    }
}