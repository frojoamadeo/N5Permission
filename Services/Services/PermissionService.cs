using AppServices.Interfaces.IServices;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Services
{
    /// <summary>
    /// PermissionServices: Manage permission operations
    /// </summary>
    public sealed class PermissionService : IPermissionService
    {
        private readonly IEmployeePermissionRepository _employeePermissionRepository;

        public PermissionService(
            IEmployeePermissionRepository employeePermissionRepository
            )
        {
            _employeePermissionRepository = employeePermissionRepository;
        }

        public async Task<IEnumerable<EmployeePermission>> GetPermissionsByEmployeeId(int employeeId)
        {
            try
            {
                return await _employeePermissionRepository.GetByEmployeeIdAsync(employeeId);
            }
            catch
            {
                throw;
            }
        }

        public async Task AddEmployeePermission(EmployeePermission employeePermission)
        {
            try 
            {
                await _employeePermissionRepository.AddAsync(employeePermission);
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateEmployeePermissions(List<EmployeePermission> employeePermissions)
        {
            try
            {
                var employeeId = employeePermissions.First().EmployeeId;
                _employeePermissionRepository.DeleteAllByEmployeeIdAsync(employeeId);
                await _employeePermissionRepository.AddBulkAsync(employeePermissions);
                await _employeePermissionRepository.SaveAsync();
            }
            catch
            {
                throw;
            }

        }
        public async Task SaveChanges()
        {
            try
            {
                await _employeePermissionRepository.SaveAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
