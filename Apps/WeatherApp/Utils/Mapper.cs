using MvcClient1.Models;
using MvcClient1.Models.ViewModel;

namespace WeatherApp.Utils
{
    public static class Mapper
    {
        public static CityData MapResponse(WeatherVMResponse weatherApiResponse)
        {
            CityData cityData = new CityData();

            if (weatherApiResponse != null)
            {
                cityData.Name = weatherApiResponse.LocationName;
                cityData.Humidity = weatherApiResponse.Humidity;
                cityData.Pressure = weatherApiResponse.Pressure;
                cityData.Temp = weatherApiResponse.TemperatureMin;
                cityData.Weather = weatherApiResponse.Desc;
            }

            return cityData;
        }
    }
}
