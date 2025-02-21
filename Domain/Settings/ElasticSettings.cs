using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Settings
{
    public sealed class ElasticSettings
    {
        public string Url { get; set; }
        public string DefaultIndex { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
