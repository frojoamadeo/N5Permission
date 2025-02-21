using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Interfaces.IServices
{
    public interface IElasticSearchService
    {
        //Create index
        public Task CreateIndexIfNotExistAsync(string indexName);

        //Add or Update EmployeePermission
        public Task<bool> AddOrUpdateEmployeePermission(EmployeePermission EmployeePermission);

        //Add or Update bulk of EmployeePermission
        public Task<bool> AddOrUpdateEmployeePermissionBulk(IEnumerable<EmployeePermission> EmployeePermissions, string indexName);

        //Get EmployeePermission
        public Task<EmployeePermission> GetEmployeePermission(string key);

        //Get all EmployeePermissions
        public Task<IEnumerable<EmployeePermission>> GetAllEmployeePermissions(string key);

        //Remove EmployeePermission
        public Task<bool> RemoveEmployeePermission(string key);

        //Remove all EmployeePermission
        public Task<long?> RemoveAllEmployeePermission();
    }
}
