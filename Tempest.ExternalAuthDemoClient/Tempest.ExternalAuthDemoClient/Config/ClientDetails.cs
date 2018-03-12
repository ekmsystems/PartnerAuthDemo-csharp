using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tempest.ExternalAuthDemoClient.Config
{
    public class ClientDetails
    {
        public string Key { get; set; }
        public string Secret { get; set; }
        public List<string> RequiredScopes { get; set; }
    }
}
