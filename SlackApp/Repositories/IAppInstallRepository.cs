using System.Threading.Tasks;
using SlackApp.Models;

namespace SlackApp.Repositories
{
    public interface IAppInstallRepository
    {
        AppInstall GetAppInstall(string userId);
        Task<int> SaveAppInstallAsync(AppInstall appInstall);
    }
}