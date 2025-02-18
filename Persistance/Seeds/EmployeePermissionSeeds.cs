using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Seeds
{
    internal sealed class EmployeePermissionSeeds
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeePermission>().HasData(
                new EmployeePermission
                {
                    Id = 1,
                    CreatedBy = "Seed",
                    CreatedOnUtc = new(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "Seed",
                    ModifiedOnUtc = new(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                    PermissionType = Domain.PermissionType.Write,
                    EmployeeId = 1,
                },
                new EmployeePermission
                {
                    Id = 2,
                    CreatedBy = "Seed",
                    CreatedOnUtc = new(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "Seed",
                    ModifiedOnUtc = new(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                    PermissionType = Domain.PermissionType.Write,
                    EmployeeId = 2
                },
                new EmployeePermission
                {
                    Id = 3,
                    CreatedOnUtc = new(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "Seed",
                    ModifiedOnUtc = new(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                    PermissionType = Domain.PermissionType.Read,
                    EmployeeId = 2
                }
            );
        }
    }
}
