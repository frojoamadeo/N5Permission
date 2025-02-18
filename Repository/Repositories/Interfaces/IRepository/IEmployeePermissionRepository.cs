using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces.IRepository
{
    public interface IEmployeePermissionRepository : IRepository<EmployeePermission>
    {
        Task<IEnumerable<EmployeePermission>> GetByEmployeeIdAsync(int employeeId);
    }
}
