using Domain.Entities;
using FakeItEasy;
using Persistance;
using Repository.Repositories.Interfaces.IRepository;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Infraestructure.Services
{
    public sealed class PermissionServiceTests
    {
        private readonly IEmployeePermissionRepository _employeePermissionRepository;

        public PermissionServiceTests()
        {
            _employeePermissionRepository = A.Fake<IEmployeePermissionRepository>();
        }

        [Fact]
        public async Task AddAsync_GivenEntity_AddToDb()
        {
            //Arrenge
            var services = new PermissionService(_employeePermissionRepository);

            var entity = new EmployeePermission();
            entity.EmployeeId = 1;
            entity.PermissionType = Domain.PermissionType.Read;

            //Act
            A.CallTo(() => _employeePermissionRepository.AddAsync(entity))
                .Returns(Task.CompletedTask);

            await services.AddEmployeePermission(entity);

            //Assert

            A.CallTo(() => _employeePermissionRepository.AddAsync(entity))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AddAsync_GivenEntityWithMissingId_ThrowException()
        {
            //Arrenge
            var services = new PermissionService(_employeePermissionRepository);

            var entity = new EmployeePermission();
            entity.EmployeeId = -1;
            entity.PermissionType = Domain.PermissionType.Read;
            var exceptionType = typeof(Exception);

            //Act
            A.CallTo(() => _employeePermissionRepository.AddAsync(entity))
                .Throws<Exception>();

            //Assert
            await Assert.ThrowsAsync(exceptionType,           
                async () => await services.AddEmployeePermission(entity)
            );
        }
    }
}
