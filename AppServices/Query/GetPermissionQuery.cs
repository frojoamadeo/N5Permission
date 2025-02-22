using AppServices.Command;
using AppServices.Interfaces.IEvents;
using AppServices.Interfaces.IServices;
using Domain.Entities;
using Domain.Events.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppServices.Query
{
    public record GetPermissionQuery(int employeeId) : IRequest<IEnumerable<EmployeePermission>>
    {
        public sealed class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery, IEnumerable<EmployeePermission>>
        {
            private readonly IPermissionService _permissionService;
            private readonly IPermissionOperationProducer _permissionOperationProducer;

            public GetPermissionQueryHandler(
                IPermissionService permissionService,
                IPermissionOperationProducer permissionOperationProducer)
            {
                _permissionService = permissionService;
                _permissionOperationProducer = permissionOperationProducer;
            }

            public async Task<IEnumerable<EmployeePermission>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
            {
                var employeeId = request.employeeId;

                var permissionOperationModel = new PermissionOperationModel();
                permissionOperationModel.NameOperation = "get";
                string message = JsonSerializer.Serialize(permissionOperationModel);
                await _permissionOperationProducer.SendPermissionOperationToTopic(message);

                return await _permissionService.GetPermissionsByEmployeeId(employeeId);
            }
        }
    }
}
