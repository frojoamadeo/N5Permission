using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Repository.Repositories.Interfaces.IRepository;

namespace Repository
{
    public static class StartupExtensions
    {
        public static IServiceCollection RegisterRepositories(
            this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeePermissionRepository, EmployeePermissionRepository>();
            return services;
        }
    }
}
