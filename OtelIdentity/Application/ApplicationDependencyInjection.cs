using Microsoft.Extensions.DependencyInjection;
using Contact.Interfaces;
using Application.Services;

namespace Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationConfigureService(this IServiceCollection services)
        {
            services.AddScoped<IUserService,UserService>();

            return services;
        }
    }
}
