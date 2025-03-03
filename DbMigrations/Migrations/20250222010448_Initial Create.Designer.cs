﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistance;

#nullable disable

namespace DbMigrations.Migrations
{
    [DbContext(typeof(PermissionDbContext))]
    [Migration("20250222010448_Initial Create")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnOrder(2);

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(1);

                    b.Property<string>("FirstName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("LastName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnOrder(4);

                    b.Property<DateTime>("ModifiedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(3);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.ToTable("employee", "permission");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedBy = "Seed",
                            CreatedOnUtc = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "Tomas",
                            LastName = "Last Name1",
                            ModifiedBy = "Seed",
                            ModifiedOnUtc = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            RowVersion = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }
                        },
                        new
                        {
                            Id = 2,
                            CreatedBy = "Seed",
                            CreatedOnUtc = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "Felipe",
                            LastName = "Last Name2",
                            ModifiedBy = "Seed",
                            ModifiedOnUtc = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            RowVersion = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }
                        },
                        new
                        {
                            Id = 3,
                            CreatedBy = "Seed",
                            CreatedOnUtc = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            FirstName = "Juan",
                            LastName = "Last Name3",
                            ModifiedBy = "Seed",
                            ModifiedOnUtc = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            RowVersion = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }
                        });
                });

            modelBuilder.Entity("Domain.Entities.EmployeePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnOrder(2);

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(1);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)")
                        .HasColumnOrder(4);

                    b.Property<DateTime>("ModifiedOnUtc")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(3);

                    b.Property<string>("PermissionType")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId", "PermissionType");

                    b.ToTable("employeepermission", "permission");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedBy = "Seed",
                            CreatedOnUtc = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            EmployeeId = 1,
                            ModifiedBy = "Seed",
                            ModifiedOnUtc = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            PermissionType = "Write",
                            RowVersion = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }
                        },
                        new
                        {
                            Id = 2,
                            CreatedBy = "Seed",
                            CreatedOnUtc = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            EmployeeId = 2,
                            ModifiedBy = "Seed",
                            ModifiedOnUtc = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            PermissionType = "Write",
                            RowVersion = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }
                        },
                        new
                        {
                            Id = 3,
                            CreatedBy = "system",
                            CreatedOnUtc = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            EmployeeId = 2,
                            ModifiedBy = "Seed",
                            ModifiedOnUtc = new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc),
                            PermissionType = "Read",
                            RowVersion = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }
                        });
                });

            modelBuilder.Entity("Domain.Entities.EmployeePermission", b =>
                {
                    b.HasOne("Domain.Entities.Employee", "Employee")
                        .WithMany("EmployeePermissions")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Domain.Entities.Employee", b =>
                {
                    b.Navigation("EmployeePermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
