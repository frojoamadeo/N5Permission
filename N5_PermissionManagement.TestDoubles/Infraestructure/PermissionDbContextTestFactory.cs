
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace N5_PermissionManagement.TestDoubles.Infraestructure
{
    public static class PermissionDbContextTestFactory
    {
        public static void Destroy(PermissionDbContext permissionDbContext)
        {
            permissionDbContext.Database.EnsureDeleted();
            permissionDbContext.Dispose();
        }

        public static PermissionDbContext CreateReadWriteDb()
        {
            var options = new DbContextOptionsBuilder<PermissionDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new PermissionDbContext(options);
            context.SetIsUnitTestRun(true);
            context.Database.EnsureCreated();
            context.SaveChanges();
            return context;
        }
    }
}
