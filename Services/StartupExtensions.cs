using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Repositories.Interfaces.IRepository;
using Repository.Repositories;
using System.Reflection;
using AppServices.Interfaces.IServices;
using Infraestructure.Services;

namespace Services
{
    public static class StartupExtensions
    {
        public static IServiceCollection RegisterServicesAndRepositories(
            this IServiceCollection services)
        {
            services.RegisterRepositories();
            RegisterServices(services);

            return services;
        }

        public static void RegisterServices(
            IServiceCollection services)
        {
            services.AddTransient<IPermissionService, PermissionService>();
        }
    }
}
