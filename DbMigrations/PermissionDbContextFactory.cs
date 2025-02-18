using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Persistance;

namespace DbMigrations
{
    public sealed class PermissionDbContextFactory : IDesignTimeDbContextFactory<PermissionDbContext>
    {
    
        public PermissionDbContext CreateNewInstance(DbContextOptions<PermissionDbContext> options)
        {
            return new PermissionDbContext(options);
        }

        PermissionDbContext IDesignTimeDbContextFactory<PermissionDbContext>.CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("Default")!;

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException($"Connection string is null or empty.", nameof(connectionString));
            }

            var assemblyName = typeof(PermissionDbContextFactory).Assembly.GetName().Name;
            var optionsBuilder = new DbContextOptionsBuilder<PermissionDbContext>();

            optionsBuilder.UseSqlServer(connectionString, opt =>
                opt.MigrationsAssembly(assemblyName)
                    .MigrationsHistoryTable("__ef_migrations_history", PermissionDbContext.SchemaPermission)
                    .CommandTimeout(600)
            );

            return CreateNewInstance(optionsBuilder.Options);
        }
    }
}
