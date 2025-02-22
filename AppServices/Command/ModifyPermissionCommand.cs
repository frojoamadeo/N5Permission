using AppServices.Interfaces.IEvents;
using AppServices.Interfaces.IServices;
using Domain;
using Domain.Entities;
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
    /// <summary>
    /// Modify Command: We update permissions from request
    /// </summary>
    public sealed class ModifyPermissionCommand : IRequest<string>
    {
        public List<string>? Permissions { get; set; }
        public int? EmployeeId { get; set; }

        public sealed class ModifyPermissionCommandHandler : IRequestHandler<ModifyPermissionCommand, string>
        {
            private readonly IPermissionOperationProducer _permissionOperationProducer;
            private readonly IPermissionService _permissionService;

            public ModifyPermissionCommandHandler(
                IPermissionOperationProducer permissionOperationProducer,
                IPermissionService permissionService
                )
            {
                _permissionOperationProducer = permissionOperationProducer;
                _permissionService = permissionService;
            }

            public async Task<string> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
            {
                if (request.EmployeeId.GetValueOrDefault() <= 0)
                {
                    return "Missing EmployeeId";
                }

                //To remove all permissions we should have another method
                if (request.Permissions == null || !request.Permissions.Any())
                {
                    return "Should add at least one permissions";
                }

                var employeePermissionList = new List<EmployeePermission>();
                
                foreach(var permissionRequested in request.Permissions)
                {
                    Enum.TryParse(permissionRequested, out PermissionType permission);

                    if(!employeePermissionList.Select(x => x.PermissionType).Contains(permission))
                    {
                        employeePermissionList.Add(new EmployeePermission() { EmployeeId = (int)request.EmployeeId!, PermissionType = permission });
                    }
                }

                await _permissionService.UpdateEmployeePermissions(employeePermissionList);

                var permissionOperationModel = new PermissionOperationModel();
                permissionOperationModel.NameOperation = "modify";
                string message = JsonSerializer.Serialize(permissionOperationModel);
                await _permissionOperationProducer.SendPermissionOperationToTopic(message);

                return string.Empty;
            }
        }
    }
}