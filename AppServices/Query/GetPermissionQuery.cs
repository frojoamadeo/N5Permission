using AppServices.Command;
using AppServices.Interfaces.IServices;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Query
{
    public record GetPermissionQuery(int employeeId) : IRequest<IEnumerable<EmployeePermission>>
    {
        public sealed class GetPermissionQueryHandler : IRequestHandler<GetPermissionQuery, IEnumerable<EmployeePermission>>
        {
            private readonly IPermissionService _permissionService;

            public GetPermissionQueryHandler(
                IPermissionService permissionService)
            {
                _permissionService = permissionService;
            }

            public async Task<IEnumerable<EmployeePermission>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
            {
                var employeeId = request.employeeId;
                return await _permissionService.GetPermissionsByEmployeeId(employeeId);
            }
        }
    }
}
