using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Interfaces.IServices
{
    public interface IPermissionService
    {
        public Task<IEnumerable<EmployeePermission>> GetPermissionsByEmployeeId(int employeeId);
        Task AddEmployeePermission(EmployeePermission employeePermission);
        Task UpdateEmployeePermissions(List<EmployeePermission> employeePermissions);
        Task SaveChanges();
    }
}
