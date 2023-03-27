using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PodTube.DataAccess.Contexts;
using PodTube.DataAccess.Seed;

namespace PodTube.DataAccess.Factory
{
    public static class DbContextRegisterExtensions
    {
        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<PodTubeDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void SeedDbWithData(this IServiceCollection services, IConfiguration configuration) {
            var options = new DbContextOptionsBuilder<PodTubeDbContext>().UseSqlServer(configuration.GetConnectionString("DefaultConnection")).Options;

            using (var dbContext = new PodTubeDbContext(options)) {
                DbInitializer.Initialize(dbContext);
            }
        }
    }
}