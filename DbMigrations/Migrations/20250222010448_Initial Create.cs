using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DbMigrations.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "permission");

            migrationBuilder.CreateTable(
                name: "employee",
                schema: "permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employeepermission",
                schema: "permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    PermissionType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeepermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employeepermission_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "permission",
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "permission",
                table: "employee",
                columns: new[] { "Id", "CreatedBy", "CreatedOnUtc", "FirstName", "LastName", "ModifiedBy", "ModifiedOnUtc" },
                values: new object[,]
                {
                    { 1, "Seed", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Tomas", "Last Name1", "Seed", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "Seed", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Felipe", "Last Name2", "Seed", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "Seed", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Juan", "Last Name3", "Seed", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                schema: "permission",
                table: "employeepermission",
                columns: new[] { "Id", "CreatedBy", "CreatedOnUtc", "EmployeeId", "ModifiedBy", "ModifiedOnUtc", "PermissionType" },
                values: new object[,]
                {
                    { 1, "Seed", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "Seed", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Write" },
                    { 2, "Seed", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Seed", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Write" },
                    { 3, "system", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Seed", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Read" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_employeepermission_EmployeeId_PermissionType",
                schema: "permission",
                table: "employeepermission",
                columns: new[] { "EmployeeId", "PermissionType" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employeepermission",
                schema: "permission");

            migrationBuilder.DropTable(
                name: "employee",
                schema: "permission");
        }
    }
}
