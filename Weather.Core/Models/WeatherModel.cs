﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Weather.Core.Structs
{
	/// <summary>
	/// Модель погоды.
	/// </summary>
	public struct WeatherModel
	{

		/// <summary>
		/// Температура.
		/// </summary>
		public float Temperature { get; set; }

		/// <summary>
		/// Влажность.
		/// </summary>
		public float Humidity { get; set; }

		/// <summary>
		/// Осадки (мм).
		/// </summary>
		public float Precipitation { get; set; }

		/// <summary>
		/// Дата измерения.
		/// </summary>
		public DateTime WeatherDate { get; set; }
	}
}
