using AppServices.Interfaces.IServices;
using Domain.Entities;
using Repository.Repositories.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Services
{
    public sealed class PermissionService : IPermissionService
    {
        private readonly IEmployeePermissionRepository _employeePermissionRepository;
        public PermissionService(IEmployeePermissionRepository employeePermissionRepository)
        {
            _employeePermissionRepository = employeePermissionRepository;
        }

        public async Task<IEnumerable<EmployeePermission>> GetPermissionsByEmployeeId(int employeeId)
        {
            return await _employeePermissionRepository.GetByEmployeeIdAsync(employeeId);
        }
    }
}
