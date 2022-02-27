using System.Threading.Tasks;

namespace OpenWeatherMapApi.Services
{
    /// <summary>
    /// Gets the weather data by city name
    /// </summary>
    public interface IWeatherRepository
    {
        Task<WeatherData> GetWeatherData(string city);
    }
}