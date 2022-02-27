using System;
using Xunit;
using OpenWeatherMapApi;
using OpenWeatherMapApi.Controllers;
using OpenWeatherMapApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WeatherDataTestProject
{
    public class WeatherDataControllerTests
    {

        WeatherForecastController _controller;
        IWeatherService _service;


        public WeatherDataControllerTests()
        {
            _service = new WeatherService(null,null);
            _controller = new WeatherForecastController(null,_service);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetWeatherData("London");

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }


    }
}
