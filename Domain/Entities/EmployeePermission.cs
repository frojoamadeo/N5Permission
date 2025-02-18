using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class EmployeePermission : BaseEntity
    {
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public PermissionType PermissionType { get; set; }
    }
}
