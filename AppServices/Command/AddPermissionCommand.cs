using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Command
{
    public sealed class AddPermissionCommand: IRequest<string>
    {
        public PermissionType? Permission { get; set; }
        public int? EmployeeId { get; set; }

        public sealed class AddPermissionCommandHAndler : IRequestHandler<AddPermissionCommand, string>
        {
            public Task<string> Handle(AddPermissionCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
