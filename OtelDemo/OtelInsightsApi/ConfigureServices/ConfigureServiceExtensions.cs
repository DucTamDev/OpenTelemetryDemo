using Application;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace OtelInsightsApi.ConfigureServices
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

            services.AddApplicationConfigureServices(configuration);
            services.AddInfrastructureConfigureServices(configuration);

            return services;
        }
    }
}
