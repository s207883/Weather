﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Core.Interfaces;
using Weather.Core.Structs;
using Weather.Core.ViewModels;

namespace Weather.BLL.Services
{
	/// <summary>
	/// Тестовый сервис погоды.
	/// </summary>
	public class FakeWeatherService : IWeatherService
	{
		public async Task<WeatherViewModel> GetWeatherByCoordinatesAsync(Coordinates coordinates)
		{
			return await GetWeatherByCoordinatesAsync(coordinates, new TimeRange { StartDate = DateTime.Now, EndDate = DateTime.Now });
		}

#pragma warning disable CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
		public async Task<WeatherViewModel> GetWeatherByCoordinatesAsync(Coordinates coordinates, TimeRange timeRange)
#pragma warning restore CS1998 // В асинхронном методе отсутствуют операторы await, будет выполнен синхронный метод
		{
			var fakeWeather = new WeatherViewModel { TimeRange = timeRange };
			var fakeWeatherData = new List<WeatherModel>();

			for (DateTime date = timeRange.StartDate; date <= timeRange.EndDate; date = date.AddDays(1))
			{
				fakeWeatherData.Add(
					new WeatherModel
					{
						Humidity = (float)(new Random().NextDouble() * 10),
						Precipitation = (float)(new Random().NextDouble() * 10),
						Temperature = (float)(new Random().NextDouble() * 10),
						WeatherDate = date,
					}
					);
			}

			fakeWeather.WeatherData = fakeWeatherData;

			return fakeWeather;
		}
	}
}
