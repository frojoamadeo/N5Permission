using AppServices.Interfaces.IEvents;
using AppServices.Interfaces.IServices;
using Domain.Entities;
using Domain.Events.Models;
using FakeItEasy;
using Shouldly;
using System.Text.Json;
using Xunit;
using static AppServices.Command.RequestPermissionCommand;

namespace AppServices.Command
{
    public sealed class AddPermissionCommandTests
    {
        public sealed class Handler
        {
            private readonly IPermissionOperationProducer _permissionOperationProducer;
            private readonly IPermissionService _permissionService;

            public Handler()
            {
                _permissionOperationProducer = A.Fake<IPermissionOperationProducer>();
                _permissionService = A.Fake<IPermissionService>();
            }

            [Fact]
            public async Task Handler_GivenRequestParameters_ShouldCallServices()
            {
                //Arrenge
                var handler = new AddPermissionCommandHandler(_permissionOperationProducer, _permissionService);

                var request = new RequestPermissionCommand();
                request.Permission = Domain.PermissionType.Read.ToString();
                request.EmployeeId = 1;

                var entity = new EmployeePermission();
                entity.EmployeeId = (int)request.EmployeeId!;
                entity.PermissionType = Domain.PermissionType.Read;
                A.CallTo(() => _permissionService.AddEmployeePermission(entity))
                    .Returns(Task.CompletedTask);

                var permissionOperationModel = new PermissionOperationModel();
                permissionOperationModel.NameOperation = "Read";
                string message = JsonSerializer.Serialize(permissionOperationModel);

                A.CallTo(() => _permissionOperationProducer.SendPermissionOperationToTopic(message))
                    .Returns(true);
                //Act
                await handler.Handle(request, CancellationToken.None);

                //Assert
                A.CallTo(() => _permissionService.AddEmployeePermission(A<EmployeePermission>.Ignored))
                    .MustHaveHappenedOnceExactly();
                A.CallTo(() => _permissionOperationProducer.SendPermissionOperationToTopic(A<string>.Ignored))
                    .MustHaveHappenedOnceExactly();
                A.CallTo(() => _permissionService.SaveChanges())
                    .MustHaveHappenedOnceExactly();
            }

            [Fact]
            public async Task Handler_GivenRequestWithMissingEmployeeId_ShouldReturnErrorMessage()
            {
                //Arrenge
                var handler = new AddPermissionCommandHandler(_permissionOperationProducer, _permissionService);

                var request = new RequestPermissionCommand();
                request.Permission = Domain.PermissionType.Read.ToString();
                request.EmployeeId = -1;

                //Act
                var response = await handler.Handle(request, CancellationToken.None);

                //Assert
                response.ShouldNotBeNull();
                response.ShouldBe("Missing EmployeeId");
            }
        }
    }
}
