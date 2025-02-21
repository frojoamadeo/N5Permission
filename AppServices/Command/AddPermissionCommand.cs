using AppServices.Interfaces.IEvents;
using AppServices.Interfaces.IServices;
using Domain;
using Domain.Events.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppServices.Command
{
    public sealed class AddPermissionCommand: IRequest<string>
    {
        public PermissionType? Permission { get; set; }
        public int? EmployeeId { get; set; }

        public sealed class AddPermissionCommandHandler : IRequestHandler<AddPermissionCommand, string>
        {
            private readonly IPermissionOperationProducer _permissionOperationProducer;
            private readonly IPermissionService _permissionService;
            public AddPermissionCommandHandler(
                IPermissionOperationProducer permissionOperationProducer,
                IPermissionService permissionService
                )
            {
                _permissionOperationProducer = permissionOperationProducer;
                _permissionService = permissionService;
            }
            public async Task<string> Handle(AddPermissionCommand request, CancellationToken cancellationToken)
            {
                if(request.EmployeeId.GetValueOrDefault() <= 0)
                {
                    return "Missing EmployeeId";
                }
                var permissions = (await _permissionService.GetPermissionsByEmployeeId((int)request.EmployeeId!)).Select(x => x.PermissionType);

                if (permissions.ToList().Contains(request.Permission ?? PermissionType.Undefinded))
                {
                    return "Permission already contained";
                }

                await _permissionService.AddEmployeePermission(new Domain.Entities.EmployeePermission()
                {
                    EmployeeId = (int)request.EmployeeId!,
                    PermissionType = (PermissionType)request.Permission!
                });

                await _permissionService.SaveChanges();

                var permissionOperationModel = new PermissionOperationModel();
                permissionOperationModel.NameOperation = "Read";
                string message = JsonSerializer.Serialize(permissionOperationModel);
                await _permissionOperationProducer.SendPermissionOperationToTopic(message);

                return string.Empty;
            }
        }
    }
}
