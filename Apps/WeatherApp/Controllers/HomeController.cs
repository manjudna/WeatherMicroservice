using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcClient1.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authentication;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;

namespace WeatherApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // [Authorize]
        public IActionResult Logout()
        {
            return SignOut("oidc", "Cookies");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CallApi()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //var content = await client.GetStringAsync("https://localhost:5002/WeatherForecast");
            var content = await client.GetStringAsync(_configuration["TestApiUrl"]);
            ViewBag.Json = JArray.Parse(content).ToString();
            return View("json");
        }

        public async Task<IActionResult> CallWeatherApi(string city)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            string apiURL = _configuration["OpenWeatherApiURL"].ToString().Replace("{city}", city);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //var content = await client.GetStringAsync("https://localhost:5002/WeatherForecast");
            var content = await client.GetStringAsync(apiURL);
            ViewBag.Json = content;//JArray.Parse(content).ToString();
            return View("json");
        }

    }
}
