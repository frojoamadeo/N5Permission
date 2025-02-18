using System.Reflection;
using Microsoft.Extensions.DependencyInjection;


namespace AppServices
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddMediatR(
            this IServiceCollection services,
            IEnumerable<Assembly> assembliesForMediatR)
        {
            foreach (var assembly in assembliesForMediatR)
            {
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            }

            return services;
        }
    }
}
