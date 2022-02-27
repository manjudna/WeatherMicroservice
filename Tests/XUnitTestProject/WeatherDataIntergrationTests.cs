using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject1
{
    public class WeatherForecastControllerTests : IClassFixture<WebApplicationFactory<OpenWeatherMapApi.Startup>>
    {
        public HttpClient Client { get; }

        public WeatherForecastControllerTests(WebApplicationFactory<OpenWeatherMapApi.Startup> fixture)
        {
            //Arrange
            Client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_Should_ReceiveOK_Forecast()
        {
            //Act
            var response = await Client.GetAsync("/WeatherData/London");

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
           
        }

        [Fact]
        public async Task Get_Should_Receive_Data_Forecast()
        {
            //Act
            var response = await Client.GetAsync("/WeatherData/London");

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var forecast = JsonConvert.DeserializeObject<WeatherData>(await response.Content.ReadAsStringAsync());
            Assert.Equal("London", forecast.LocationName);
        }

        [Fact]
        public async Task Get_Should_ReceiveData_NotNULL_Forecast()
        {
            //Act
            var response = await Client.GetAsync("/WeatherData/London");

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var forecast = JsonConvert.DeserializeObject<WeatherData>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(response);
            Assert.Equal("London", forecast.LocationName);
            Assert.NotNull(forecast.Humidity.ToString());
            Assert.NotNull(forecast.Sunrise.ToString());
            Assert.NotNull(forecast.Sunset.ToString());
            Assert.NotNull(forecast.Pressure.ToString());
            Assert.NotNull(forecast.TemperatureC.ToString());
            Assert.NotNull(forecast.TemperatureMax.ToString());
            Assert.NotNull(forecast.TemperatureMin.ToString());
            Assert.NotNull(forecast.Desc.ToString());
        }


        [Fact]
        public async Task Get_Should_ThrowNotFound_Forecast()
        {
            //Act
            var response = await Client.GetAsync("/WeatherData/sdfsddsdf");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Get_Should_ThrowBadRequest_Forecast()
        {
            //Act
            var response = await Client.GetAsync("/WeatherData/*&*&2d");

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            
        }



    }
}
