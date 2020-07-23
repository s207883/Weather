using System;
using System.Threading.Tasks;
using Weather.Core.Structs;
using Weather.Core.ViewModels;

namespace Weather.Core.Interfaces
{
	public interface IWeatherService
	{
		/// <summary>
		/// Получить погоду по координатам.
		/// (Без указания даты, погода выводится на 7 дней.)
		/// </summary>
		/// <param name="coordinates">Координаты.</param>
		/// <returns>Модель прогноза погоды.</returns>
		public Task<WeatherViewModel> GetWeatherByCoordinatesAsync(Coordinates coordinates);

		/// <summary>
		/// Получить погоду по координатам.
		/// </summary>
		/// <param name="coordinates">Координаты.</param>
		/// <param name="timeRange">Временной промежуток.</param>
		/// <returns>Модель прогноза погоды.</returns>
		public Task<WeatherViewModel> GetWeatherByCoordinatesAsync(Coordinates coordinates, TimeRange timeRange);
	}
}
