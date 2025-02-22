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
    public sealed class RequestPermissionCommand: IRequest<string>
    {
        public string? Permission { get; set; }
        public int? EmployeeId { get; set; }

        public sealed class AddPermissionCommandHandler : IRequestHandler<RequestPermissionCommand, string>
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

            public async Task<string> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
            {
                if(request.EmployeeId.GetValueOrDefault() <= 0)
                {
                    return "Missing EmployeeId";
                }
                var permissions = (await _permissionService.GetPermissionsByEmployeeId((int)request.EmployeeId!)).Select(x => x.PermissionType);

                Enum.TryParse(request.Permission, out PermissionType permission);
                if (permissions.ToList().Contains(permission))
                {
                    return "Permission already contained";
                }

                if(permission == PermissionType.Undefinded)
                {
                    return "Permission incorrect";
                }

                await _permissionService.AddEmployeePermission(new Domain.Entities.EmployeePermission()
                {
                    EmployeeId = (int)request.EmployeeId!,
                    PermissionType = permission!
                });

                await _permissionService.SaveChanges();

                var permissionOperationModel = new PermissionOperationModel();
                permissionOperationModel.NameOperation = "request";
                string message = JsonSerializer.Serialize(permissionOperationModel);
                await _permissionOperationProducer.SendPermissionOperationToTopic(message);

                return string.Empty;
            }
        }
    }
}
