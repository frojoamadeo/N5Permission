using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServices.Interfaces.IEvents
{
    public interface IPermissionOperationProducer
    {
        public Task<bool> SendPermissionOperationToTopic(string message);
    }
}
