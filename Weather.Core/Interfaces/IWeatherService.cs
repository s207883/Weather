using System;
using Weather.Core.Structs;
using Weather.Core.ViewModels;

namespace Weather.Core.Interfaces
{
	public interface IWeatherService
	{
		/// <summary>
		/// Получить погоду по координатам.
		/// </summary>
		/// <param name="coordinates">Координаты.</param>
		/// <returns>Модель прогноза погоды.</returns>
		public WeatherViewModel GetWeatherByCoordinates(Coordinates coordinates);

		/// <summary>
		/// Получить погоду по координатам.
		/// </summary>
		/// <param name="coordinates">Координаты.</param>
		/// <param name="timeSpan">Временной промежуток.</param>
		/// <returns>Модель прогноза погоды.</returns>
		public WeatherViewModel GetWeatherByCoordinates(Coordinates coordinates, TimeSpan timeSpan);
	}
}
