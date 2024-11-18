using Appliction.Configurations.Interfaces.Services;
using Appliction.Services;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;
using System.Reflection;

namespace Appliction
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationConfigureService(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
