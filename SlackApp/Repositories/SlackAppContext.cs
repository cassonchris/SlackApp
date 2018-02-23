using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SlackApp.Config;
using SlackApp.Models.SlackWebApi;

namespace SlackApp.Repositories
{
    public class SlackAppContext : DbContext
    {
        private readonly SlackAppConfig _config;

        public SlackAppContext(DbContextOptions<SlackAppContext> dbContextOptions, IOptions<SlackAppConfig> configOptions)
            : base(dbContextOptions)
        {
            _config = configOptions.Value;
        }

        public DbSet<AppInstall> AppInstalls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config.ConnectionStrings.SlackAppConnection);
        }
    }
}
