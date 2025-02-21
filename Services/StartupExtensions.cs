using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.Repositories.Interfaces.IRepository;
using Repository.Repositories;
using System.Reflection;
using AppServices.Interfaces.IServices;
using Infraestructure.Services;
using AppServices.Interfaces.IEvents;
using Infraestructure.Events.Producer;

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
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IElasticSearchService, ElasticSearchService>();
            
            //EventServices
            services.AddScoped<IPermissionOperationProducer, PermissionOperationProducer>();
        }
    }
}
