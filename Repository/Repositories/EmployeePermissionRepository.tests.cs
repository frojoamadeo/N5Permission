using Domain.Entities;
using FakeItEasy;
using N5_PermissionManagement.TestDoubles.Infraestructure;
using Persistance;
using Repository.Repositories.Interfaces.IRepository;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Repository.Repositories
{
    public sealed class EmployeePermissionRepositoryTests
    {
        private readonly PermissionDbContext _permissionDbContext;
        private readonly PermissionDbContext _permissionDbContextFake;


        public EmployeePermissionRepositoryTests()
        {
            _permissionDbContext = PermissionDbContextTestFactory.CreateReadWriteDb();
        }

        [Fact]
        public async Task AddAsync_GivenEntity_AddToDb()
        {
            //Arrenge
            var repository = new EmployeePermissionRepository(_permissionDbContext);

            var entity = new EmployeePermission();
            entity.EmployeeId = 100;
            entity.PermissionType = Domain.PermissionType.Read;

            //Act

            await repository.AddAsync(entity);
            await _permissionDbContext.SaveChangesAsync();
            //Assert

            var newEntity = _permissionDbContext.EmployeePermissions
                .FirstOrDefault(x => x.Id == entity.Id);

            newEntity.ShouldNotBeNull();
            newEntity.PermissionType.ShouldBe(Domain.PermissionType.Read);
        }

        [Fact]
        public async Task AddAsync_GivenNoEntity_ThrowException()
        {
            //Arrenge
            var repository = new EmployeePermissionRepository(_permissionDbContext);

            var exceptionType = typeof(System.ArgumentNullException);

            //Act
            //Assert
            await Assert.ThrowsAsync(exceptionType,
                async () => await repository.AddAsync(null)
            );
        }
    }
}
