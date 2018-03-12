using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Tempest.ExternalAuthDemoClient.Config;
using Tempest.ExternalAuthDemoClient.Models;

namespace Tempest.ExternalAuthDemoClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ClientDetails _clientDetails;

        public HomeController(IConfiguration configuration, IOptions<ClientDetails> clientDetails)
        {
            _configuration = configuration;
            _clientDetails = clientDetails.Value;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/")]
        public IActionResult IndexPost()
        {
            var gatewayUrl = _configuration.GetValue<string>("GatewayUrl");
            var currentUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            var redirectUri = $"{currentUrl}/consented&prompt=login&nonce={Guid.NewGuid()}&response_type=code";
            var redirectParams = new List<string>
            {
                $"client_id={_clientDetails.Key}",
                $"scope={string.Join(" ", _clientDetails.RequiredScopes)}",
                $"redirect_uri={redirectUri}"
            };
            return Redirect($"{gatewayUrl}/connect/authorize?{string.Join("&", redirectParams)}");
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
