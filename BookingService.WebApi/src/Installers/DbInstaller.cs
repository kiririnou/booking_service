using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.WebApi.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Models.dbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseNpgsql(
                        configuration.GetConnectionString("DefaultConnection")
                    )
            );
        }
    }
}