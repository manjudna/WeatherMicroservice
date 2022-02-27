﻿using System;

namespace OpenWeatherMapApi.Services
{
    /// <summary>
    /// maps the api response to business model
    /// </summary>
    public class WeatherMapper
    {
        public static WeatherData MapWeatherObjects(WeatherResponse weatherResponse , WeatherData weatherData)
        {
            if (weatherResponse == null)
                return null;

            weatherData.LocationName = weatherResponse.Name;
            weatherData.Humidity = weatherResponse.Main.Humidity;
            weatherData.Pressure = weatherResponse.Main.Pressure;
            weatherData.Sunrise = weatherResponse.Sys.SunRise;
            weatherData.Sunset = weatherResponse.Sys.SunSet;
            weatherData.TemperatureC = Convert.ToInt32(weatherResponse.Main.Temp);
            weatherData.TemperatureMax = Convert.ToInt32(weatherResponse.Main.Temp_Max);
            weatherData.TemperatureMin = Convert.ToInt32(weatherResponse.Main.Temp_Min);
            weatherData.Desc = weatherResponse.Weather[0].Main;
            return weatherData;
        }
    }
}