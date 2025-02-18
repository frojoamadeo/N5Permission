using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Persistance.Configurations.Extensions;

namespace Persistance.Configurations
{
    public sealed partial class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable(nameof(Employee).ToLower()!, PermissionDbContext.SchemaPermission);
            builder.ConfigureBaseEntity(useIdentityColumn: true);

            builder.Property(x => x.FirstName).HasMaxLength(200); ;
            builder.Property(x => x.LastName).HasMaxLength(200); ;
        }
    }
}
