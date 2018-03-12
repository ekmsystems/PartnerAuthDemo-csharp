using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using Tempest.ExternalAuthDemoClient.Config;
using Tempest.ExternalAuthDemoClient.Models;

namespace Tempest.ExternalAuthDemoClient.Controllers
{
    public class ConsentedController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ClientDetails _clientDetails;

        public ConsentedController(IConfiguration configuration, IOptions<ClientDetails> clientDetails)
        {
            _configuration = configuration;
            _clientDetails = clientDetails.Value;
        }
        [HttpGet("consented")]
        public async Task<IActionResult> Index(ConsentedViewModel viewModel)
        {
            var gatewayUrl = _configuration.GetValue<string>("GatewayUrl");
            var redirectUri = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/consented";
            var requestBodyParams = new List<string>
            {
                $"client_id={_clientDetails.Key}",
                $"client_secret={_clientDetails.Secret}",
                $"code={viewModel.Code}",
                "grant_type=authorization_code",
                $"redirect_uri={redirectUri}"
            };

            var restClient = new RestClient($"{gatewayUrl}/connect/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", string.Join("&", requestBodyParams), ParameterType.RequestBody);

            IRestResponse response = restClient.Execute(request);

            dynamic data = JsonConvert.DeserializeObject(response.Content);

            viewModel.AccessToken = data.access_token;
            viewModel.ExpiresIn = data.expires_in;
            viewModel.TokenType = data.token_type;
            viewModel.RefreshToken = data.refresh_token;

            return View(viewModel);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenModel model)
        {
            var gatewayUrl = _configuration.GetValue<string>("GatewayUrl");
            var requestBodyParams = new List<string>
            {
                "grant_type=refresh_token",
                $"client_id={_clientDetails.Key}",
                $"client_secret={_clientDetails.Secret}",
                $"refresh_token={model.RefreshToken}"
            };

            var restClient = new RestClient($"{gatewayUrl}/connect/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", string.Join("&", requestBodyParams), ParameterType.RequestBody);

            IRestResponse response = restClient.Execute(request);

            dynamic data = JsonConvert.DeserializeObject(response.Content);

            var viewModel = new ConsentedViewModel();

            viewModel.AccessToken = data.access_token;
            viewModel.ExpiresIn = data.expires_in;
            viewModel.TokenType = data.token_type;
            viewModel.RefreshToken = data.refresh_token;

            return View("Index", viewModel);
        }
    }
}