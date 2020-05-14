using BookingService.WebApi.Contracts.V1;
using BookingService.WebApi.Services;
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

            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IUserService, UserService>();            
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<IReservationService, ReservationService>();
        }
    }
}