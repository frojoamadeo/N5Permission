using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Seeds
{
    internal sealed class EmployeeSeeds
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "Tomas",
                    LastName = "Last Name1",
                    CreatedBy = "Seed",
                    CreatedOnUtc = new(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "Seed",
                    ModifiedOnUtc = new(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Felipe",
                    LastName = "Last Name2",
                    CreatedBy = "Seed",
                    CreatedOnUtc = new(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "Seed",
                    ModifiedOnUtc = new(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "Juan",
                    LastName = "Last Name3",
                    CreatedBy = "Seed",
                    CreatedOnUtc = new(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "Seed",
                    ModifiedOnUtc = new(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                }
            );
        }
    }
}