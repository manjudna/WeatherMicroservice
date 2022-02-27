using OpenWeatherMapApi.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject
{
    public class WeatherValidationTests
    {
        private readonly WeatherValidation weatherValidationTest;

        public WeatherValidationTests()
        {
            weatherValidationTest = new WeatherValidation();
        }
            
        [Fact]
        public void ValidateCityTest_should_Return_Is_Valid()
        {
           Assert.True( weatherValidationTest.IsValidCity("Paris"));

        }

        [Fact]
        public void ValidateCityTest_should_Return_Is_InValid()
        {
            Assert.False(weatherValidationTest.IsValidCity("*&(*FA"));

        }






    }
}
