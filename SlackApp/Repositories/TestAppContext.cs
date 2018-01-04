using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SlackApp.Config;
using SlackApp.Models.SlackWebApi;

namespace SlackApp.Repositories
{
    public class TestAppContext : DbContext
    {
        private readonly TestAppConfig _config;

        public TestAppContext(DbContextOptions<TestAppContext> dbContextOptions, IOptions<TestAppConfig> configOptions)
            : base(dbContextOptions)
        {
            _config = configOptions.Value;
        }

        public DbSet<AppInstall> AppInstalls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_config.ConnectionStrings.TestAppConnection);
        }
    }
}
