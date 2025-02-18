using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Configurations.Extensions
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureBaseEntity<T>(this EntityTypeBuilder<T> builder, bool useIdentityColumn = true)
        where T : BaseEntity
        {
            builder.HasKey(x => x.Id);

            if (useIdentityColumn)
            {
                builder.Property(x => x.Id).UseIdentityColumn(1, 1);
            }
            else
            {
                builder.Property(x => x.Id).ValueGeneratedNever();
            }

            builder.Property(x => x.CreatedBy).HasMaxLength(128);
            builder.Property(x => x.CreatedOnUtc);
            builder.Property(x => x.ModifiedBy).HasMaxLength(128);
            builder.Property(x => x.ModifiedOnUtc);
            builder.Property(x => x.RowVersion).IsRowVersion().HasConversion<byte[]>();
        }
    }
}
