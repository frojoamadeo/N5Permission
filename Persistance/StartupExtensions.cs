using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddEntityFramewrokService(
            this IServiceCollection services,
            string connectionName)
        {
            services.AddDbContext<PermissionDbContext>(options => options.UseSqlServer(connectionName));
            services.AddScoped<DbContext, PermissionDbContext>();
            return services;
        }
    }
}
