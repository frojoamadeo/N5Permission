using System;
namespace Domain.Entities
{
    public sealed class Employee : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public List<EmployeePermission> EmployeePermissions { get; set; } = new();

    }
}
