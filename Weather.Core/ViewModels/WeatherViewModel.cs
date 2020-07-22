using System;
using System.Collections.Generic;
using Weather.Core.Structs;

namespace Weather.Core.ViewModels
{
	/// <summary>
	/// Модель прогноза погоды.
	/// </summary>
	public class WeatherViewModel
	{
		/// <summary>
		/// Погодные данные.
		/// </summary>
		public IEnumerable<WeatherModel> WeatherData { get; set; }

		/// <summary>
		/// Временной промежуток.
		/// </summary>
		public TimeRange TimeRange { get; set; }
	}
}
