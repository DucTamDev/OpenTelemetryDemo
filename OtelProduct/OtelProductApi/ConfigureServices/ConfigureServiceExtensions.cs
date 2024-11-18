using Appliction;
using Infrastructure;
using Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OtelProductApi.ConfigureServices
{
    public static class ConfigureServiceExtensions
    {
        public static IServiceCollection AddConfigureServiceExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            int.TryParse(configuration.GetSection("ConnectionStrings:DefaultTimeoutInSeconds").Value, out int defaultSqlServerTimeoutInSeconds);

            services.AddDbContext<AppDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    opt => opt.CommandTimeout(TimeSpan.FromSeconds(defaultSqlServerTimeoutInSeconds).Seconds)),
                    ServiceLifetime.Transient);

            services.AddApplicationConfigureService();
            services.AddInfrastructureConfigureService();

            return services;
        }
    }
}
