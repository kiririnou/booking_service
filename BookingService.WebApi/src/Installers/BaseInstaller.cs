using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.WebApi.Installers
{
    // TODO: Rename
    public class BaseInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddRouting(options => options.LowercaseUrls = true);
        }
    }
}