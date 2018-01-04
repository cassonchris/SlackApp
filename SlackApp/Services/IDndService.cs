using System.Threading.Tasks;

namespace SlackApp.Services
{
    public interface IDndService
    {
        Task<bool> SetSnooze(int duration, string accessToken);
    }
}