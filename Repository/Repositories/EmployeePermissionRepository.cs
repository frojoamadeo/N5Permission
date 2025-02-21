using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public sealed class EmployeePermissionRepository : IEmployeePermissionRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<EmployeePermission> _dbSet;

        public EmployeePermissionRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<EmployeePermission>();
        }

        public async Task AddAsync(EmployeePermission entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public Task DeleteAsync(EmployeePermission entity)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeePermission> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeePermission>> ListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeePermission>> ListAsync(Expression<Func<EmployeePermission, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(EmployeePermission entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmployeePermission>> GetByEmployeeIdAsync(int employeeId)
        {
            try
            {
                return await _dbSet.Where(x => x.EmployeeId == employeeId).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
