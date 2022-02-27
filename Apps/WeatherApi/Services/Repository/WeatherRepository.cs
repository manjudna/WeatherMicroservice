using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenWeatherMapApi.Services
{

    /// <summary>
    /// Weather Repository Class.
    /// </summary>
    /// <seealso cref="OpenWeatherMapApi.Services.IWeatherRepository" />
    public class WeatherRepository : IWeatherRepository
    {
        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Gets or sets the application settings.
        /// </summary>
        /// <value>
        /// The application settings.
        /// </value>
        private AppSettings AppSettings { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherRepository"/> class.
        /// </summary>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="settings">The settings.</param>
        public WeatherRepository(IMemoryCache memoryCache, IOptions<AppSettings> settings)
        {
            _memoryCache = memoryCache;
            AppSettings = settings.Value;
        }

        /// <summary>
        /// Get the weather info by city name from OpenAPI
        /// </summary>
        /// <param name="city">The city.</param>
        /// <returns></returns>
        public async Task<WeatherData> GetWeatherData(string city)
        {

            string cacheKey = city.ToLower();

            if (!_memoryCache.TryGetValue(cacheKey, out WeatherData weatherForecast))
            {
                var noofret = 0;

                var policy = Policy.
                    Handle<Exception>().
                    OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                    .RetryAsync(3, (ex, retryCount) =>
                    {
                        noofret = retryCount;
                    });

                string apiURL = AppSettings.OpenWeatherApiURL.Replace("{city}", city);
                

                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await policy.ExecuteAsync(
                    () => httpClient.GetAsync(apiURL));
                if (response.IsSuccessStatusCode)
                {
                    var resultsfromapi = response.Content.ReadAsStringAsync();
                    var content = JsonConvert.DeserializeObject<JToken>(resultsfromapi.Result);
                    var weatherData1 = content.ToObject<WeatherResponse>();
                    if (weatherForecast == null)
                        weatherForecast = new WeatherData();

                    weatherForecast = WeatherMapper.MapWeatherObjects(weatherData1, weatherForecast);
                    //add redis cache here ideally

                    //TODO::to be moved to Seperate class to make use of memory related operations 
                    var cacheExpiryOption = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddHours(1),
                        Priority = CacheItemPriority.Low,
                        SlidingExpiration = TimeSpan.FromMinutes(1)
                    };
                    _memoryCache.Set(cacheKey, weatherForecast, cacheExpiryOption);

                }

            }

            return weatherForecast;

        }
    }
}