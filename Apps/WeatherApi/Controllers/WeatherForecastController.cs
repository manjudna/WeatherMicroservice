using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenWeatherMapApi.Models.Error;
using OpenWeatherMapApi.Services;
using RestSharp;

namespace OpenWeatherMapApi.Controllers
{
    /// <summary>
    /// Weather forecast controller class.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<WeatherForecastController> _logger;


        /// <summary>
        /// The weather service
        /// </summary>
        private readonly IWeatherService _weatherService;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="weatherService">The weather service.</param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger,IWeatherService weatherService)
        {
            _weatherService = weatherService;
            _logger = logger;
        }


        /// <summary>
        /// Gets the weatherdata from OpenMap Weather API.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("/WeatherData/{city}")]
        public async Task<IActionResult> GetWeatherData(string city)
        {
            _logger.Log(LogLevel.Information, "GetWeatherData function entered");

            if (city == null )
                return BadRequest("City not found");

            var weatherdata = await _weatherService.GetWeatherData(city);      
            
            if(!weatherdata.IsSuccess)
                return BadRequest();

            if (weatherdata.weatherData == null)
                return NotFound(new ErrorDetails {Message="City Not Found",StatusCode = Convert.ToInt32( System.Net.HttpStatusCode.NotFound) });

            return Ok(weatherdata.weatherData);
        }

    }
}
