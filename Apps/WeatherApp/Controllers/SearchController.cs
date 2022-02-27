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
using Newtonsoft.Json;
using MvcClient1.Models.ViewModel;
using WeatherApp.Utils;

namespace WeatherApp.Controllers
{
    /// <summary>
    /// Search controller
    /// </summary>
    /// <seealso cref="Controller" />
    public class SearchController : Controller
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<SearchController> _logger;
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="configuration">The configuration.</param>
        public SearchController(ILogger<SearchController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Searches the city.
        /// </summary>
        /// <returns></returns>
        public IActionResult SearchCity()
        {
            var viewModel = new Search();
            return View(viewModel);
        }

        /// <summary>
        /// Searches the city.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SearchCity(Search model)
        {
            // If the model is valid, consume the Weather API to bring the data of the city
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "Search", new { city = model.CityName });
            }
            return View(model);
        }

        /// <summary>
        /// Cities the specified city.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <returns></returns>
        public async Task<IActionResult> City(string city)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            string apiURL = _configuration["TestApiUrl"].ToString().Replace("{city}", city);

            var client = new HttpClient();

            string content = string.Empty;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            try
            {
                //var content = await client.GetStringAsync("https://localhost:5002/WeatherForecast");
                content = await client.GetStringAsync(apiURL);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            var contentDeserialized = JsonConvert.DeserializeObject<JToken>(content);

            WeatherVMResponse weatherApiResponse = null;

            if (contentDeserialized != null)
            {
                // Deserialize the JToken object into our WeatherResponse Class
                weatherApiResponse = contentDeserialized.ToObject<WeatherVMResponse>();
            }

            CityData cityData = Mapper.MapResponse(weatherApiResponse);

            return View(cityData);
        }

    }
}
