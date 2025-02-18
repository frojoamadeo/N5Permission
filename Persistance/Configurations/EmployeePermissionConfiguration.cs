using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Configurations.Extensions;

namespace Persistance.Configurations
{
    public sealed partial class EmployeePermissionConfiguration : IEntityTypeConfiguration<EmployeePermission>
    {
        public void Configure(EntityTypeBuilder<EmployeePermission> builder)
        {
            builder.ToTable(nameof(EmployeePermission).ToLower()!, PermissionDbContext.SchemaPermission);
            builder.ConfigureBaseEntity(useIdentityColumn: true);

            builder.Property(x => x.EmployeeId);
            builder.Property(x => x.PermissionType).HasConversion<string>().HasMaxLength(64);

            // Foreign keys
            builder.HasOne(a => a.Employee)
                .WithMany(b => b.EmployeePermissions)
                .HasForeignKey(c => c.EmployeeId);

            //Indexes
            builder.HasIndex(x => new { x.EmployeeId, x.PermissionType });
        }
    }
}
