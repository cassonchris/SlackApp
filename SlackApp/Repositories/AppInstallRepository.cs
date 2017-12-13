using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SlackApp.Models;
using SQLitePCL;

namespace SlackApp.Repositories
{
    public class AppInstallRepository : IAppInstallRepository
    {
        private readonly TestAppContext _context;

        public AppInstallRepository(TestAppContext context)
        {
            _context = context;
        }

        public AppInstall GetAppInstall(string userId)
        {
            return _context.AppInstalls.FirstOrDefault(ai =>
                ai.UserId.Equals(userId, StringComparison.CurrentCultureIgnoreCase));
        }

        public async Task<int> SaveAppInstallAsync(AppInstall appInstall)
        {
            await _context.AppInstalls.AddAsync(appInstall);
            return await _context.SaveChangesAsync();
        }
    }
}
