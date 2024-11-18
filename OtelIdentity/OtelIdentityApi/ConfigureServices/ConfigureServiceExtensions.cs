using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace OtelIdentityApi.ConfigureServices
{
    public static class ConfigureServiceExtensions
    {
        public static IServiceCollection AddConfigureServiceExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            int.TryParse(configuration.GetSection("ConnectionStrings:DefaultSqlServerTimeoutInSeconds").Value, out int defaultSqlServerTimeoutInSeconds);

            services.AddDbContext<ApplicationContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    opt => opt.CommandTimeout(TimeSpan.FromSeconds(defaultSqlServerTimeoutInSeconds).Seconds)),
                    ServiceLifetime.Transient);

            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationContext>();

            return services;
        }
    }
}
