using ForecastApp.OpenWeatherMapModels;
using MvcClient1.Models.OpenWeatherMapModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcClient1.Models
{

    public class WeatherApiResponse
    {
        public Coord Coord { get; set; }
        public List<Weather> Weather { get; set; }
        public string Base { get; set; }
        public Main Main { get; set; }
        public int Visibility { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
        public int Dt { get; set; }
        public Sys Sys { get; set; }
        public int Timezone { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }

    }

    public class WeatherVMResponse
    {
        public int TemperatureC { get; set; }
        public int TemperatureMax { get; set; }
        public int TemperatureMin { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public string LocationName { get; set; }
        public string Desc { get; set; }
       
    }
}
