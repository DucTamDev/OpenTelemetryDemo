using Contact.Interfaces.HttpServices.Product;
using Infrastructure.HttpServices.ProductApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructureConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddScoped<IProductApi, ProductApi>();

            return services;
        }
    }
}
