using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Events.Consumer.Models
{
    public sealed class PermissionOperationModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? NameOperation { get; set; }
    }
}
