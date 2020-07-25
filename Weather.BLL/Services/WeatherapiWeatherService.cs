using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Core.Interfaces;
using Weather.Core.Structs;
using Weather.Core.ViewModels;

namespace Weather.BLL.Services
{
    /// <summary>
    /// Сервис погоды от <see href="https://www.weatherapi.com/">https://www.weatherapi.com/</see>.
    /// Документация по <see href="https://www.weatherapi.com/docs/">API</see>.
    /// Сервис ограничивает просмотр истории только 7 днями.
    /// </summary>
    public class WeatherapiWeatherService : IWeatherService
    {
        private const string _apiKey = "c218e4fc64c14f0dbe2202715202407";
        private const string _baseAddress = "http://api.weatherapi.com/v1";
        private readonly ILogger<WeatherapiWeatherService> _logger;

        public WeatherapiWeatherService(ILogger<WeatherapiWeatherService> logger)
        {
            _logger = logger ??throw new ArgumentNullException(nameof(logger));
        }

        public async Task<WeatherViewModel> GetWeatherByCoordinatesAsync(Coordinates coordinates)
        {
            try
            {
                var currentDateTime = DateTime.Now;
                var apiResuestString = _baseAddress
                    .AppendPathSegment("/forecast.json")
                    .SetQueryParams(new
                    {
                        key = _apiKey,
                        q= $"{coordinates.Latitude},{coordinates.Longitude}",
                        dt=currentDateTime.ToString("yyyy-MM-dd"),
                        days=1,
                        lang="ru"
                    });

                var apiResponse = await apiResuestString.GetAsync();

                var resultContent = await apiResponse.Content.ReadAsStringAsync();

                var parsedResult = JsonConvert.DeserializeObject<WeatherApiResponseModel>(resultContent);

                var todayForecast = parsedResult.forecast.forecastday.Count > 0 ? parsedResult.forecast.forecastday[0].day : null;
                if (todayForecast is null)
                {
                    return default;
                }

                var weatherData = new List<WeatherModel>
                {
                    new WeatherModel { WeatherDate = currentDateTime, Humidity = todayForecast.avghumidity, Pressure = todayForecast.pressure_mb, Temperature = todayForecast.avgtemp_c }
                };

                return new WeatherViewModel { TimeRange = new TimeRange(currentDateTime, currentDateTime), WeatherData = weatherData };
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, nameof(GetWeatherByCoordinatesAsync));
                return default;
            }
        }

        public async Task<WeatherViewModel> GetWeatherByCoordinatesAsync(Coordinates coordinates, TimeRange timeRange)
        {
            try
            {
                //"API key is limited to get history data within last 8 days only. Upgrade to Gold or Platinum plans to lift this limit."
                if (timeRange.StartDate < DateTime.Now.AddDays(-7))
                {
                    timeRange.StartDate = DateTime.Now.AddDays(-7);
                }
                if (timeRange.EndDate > DateTime.Now)
                {
                    timeRange.EndDate = DateTime.Now;
                }

                var result = new WeatherViewModel { TimeRange = timeRange };
                var forecastList = new List<WeatherModel>();

                //Так как АПИ не дает возможность сделать запрос с выборкой по нескольким дням, приходится спамить сервис запросами по дням. 
                for (var date = timeRange.StartDate; date < timeRange.EndDate; date = date.AddDays(1))
                {
                    var apiResuestString = _baseAddress
                        .AppendPathSegment("/history.json")
                        .SetQueryParams(new
                        {
                            key = _apiKey,
                            q = $"{coordinates.Latitude},{coordinates.Longitude}",
                            dt = date.ToString("yyyy-MM-dd"),
                            lang = "ru"
                        });

                    var apiResponse = await apiResuestString.GetAsync();

                    var resultContent = await apiResponse.Content.ReadAsStringAsync();

                    var parsedResult = JsonConvert.DeserializeObject<WeatherApiResponseModel>(resultContent);

                    var forecast = parsedResult.forecast.forecastday.FirstOrDefault()?.day;

                    if (forecast != default)
                    {
                        
                        forecastList.Add(
                            new WeatherModel { 
                                Humidity = forecast.avghumidity,
                                Pressure = forecast.pressure_mb,
                                Temperature = forecast.avgtemp_c,
                                WeatherDate = date
                            });
                    }
                }

                result.WeatherData = forecastList;

                return result;
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, nameof(GetWeatherByCoordinatesAsync));
                return default;
            }
        }
    }

    #region API ResponseModels

    class Location
    {

        /// <summary>
        /// Location name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Region or state of the location, if availa
        /// </summary>
        public string region { get; set; }
        /// <summary>
        /// Location country
        /// </summary>
        public string country { get; set; }
    }

    class Day
    {
        /// <summary>
        /// Average temperature in celsius for the day
        /// </summary>
        public float avgtemp_c { get; set; }
        /// <summary>
        /// Maximum wind speed in kilometer per hour
        /// </summary>
        public float maxwind_kph { get; set; }
        /// <summary>
        /// Total precipitation in milimeter
        /// </summary>
        public float totalprecip_mm { get; set; }
        /// <summary>
        /// Pressure in millibars
        /// </summary>
        public float pressure_mb { get; set; }
        /// <summary>
        /// Average humidity as percentage
        /// </summary>
        public float avghumidity { get; set; }
    }

    class ForecastdayItem
    {
        public string date { get; set; }
        public Day day { get; set; }
    }

    class Forecast
    {
        public List<ForecastdayItem> forecastday { get; set; }
    }

    class WeatherApiResponseModel
    {
        public Location location { get; set; }
        public Forecast forecast { get; set; }
    }

    #endregion
}
