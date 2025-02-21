using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Seeds;

namespace Persistance
{
    public sealed class PermissionDbContext : DbContext
    {
        public const string SchemaPermission = "permission";
        public bool IsUnitTestRun { get; private set; }

        public PermissionDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeePermission> EmployeePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PermissionDbContext).Assembly);

            //Seeds
            AddDataSeeds(modelBuilder);
        }

        private void AddDataSeeds(ModelBuilder modelBuilder)
        {
            new EmployeeSeeds().Seed(modelBuilder);
            new EmployeePermissionSeeds().Seed(modelBuilder);
        }

        public void SetIsUnitTestRun(bool isTestRun)
        {
            IsUnitTestRun = isTestRun;
        }
    }
}
