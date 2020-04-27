using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuoteMe.Contracts.Interfaces.Services;
using QuoteMe.Contracts.Services;
using QuoteMe.DB;

namespace QuoteMe.Server.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IAddressService, AddressService>();

        }
    }
}
