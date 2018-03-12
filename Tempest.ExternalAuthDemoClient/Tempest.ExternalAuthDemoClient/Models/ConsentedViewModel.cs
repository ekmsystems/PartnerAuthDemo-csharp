using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tempest.ExternalAuthDemoClient.Models
{
    public class ConsentedViewModel
    {
        public string Code { get; set; }
        public string Scope { get; set; }
        public string AccessToken { get; set; }
        public string ExpiresIn { get; set; }
        public string TokenType { get; set; }
        public string RefreshToken { get; set; }
    }
}
