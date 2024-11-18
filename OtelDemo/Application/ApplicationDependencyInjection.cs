using Application.Services;
using Contact.Interfaces.AppServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IOtelTraceService, OtelTraceService>();
            services.AddScoped<IOtelMetricService, OtelMetricService>();

            return services;
        }
    }
}
