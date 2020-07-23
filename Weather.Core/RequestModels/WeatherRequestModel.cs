using System;
using System.ComponentModel.DataAnnotations;

namespace Weather.Core.RequestModels
{
	/// <summary>
	/// Модель запроса данных о погоде.
	/// </summary>
	public class WeatherRequestModelBase
	{
		/// <summary>
		/// Широта.
		/// </summary>
		[Required(AllowEmptyStrings = false, ErrorMessage = "Не указана широта")]
		[Range(1, 360, ErrorMessage = "Недопустимая широта")]
		public float Latitude { get; set; }

		/// <summary>
		/// Долгота.
		/// </summary>
		[Required(AllowEmptyStrings = false, ErrorMessage = "Не указана долгота")]
		[Range(1, 360, ErrorMessage = "Недопустимая долгота")]
		public float Longitude { get; set; }
	}

	public class WeatherRequestModelWithDate : WeatherRequestModelBase
	{
		/// <summary>
		/// Начальная дата.
		/// </summary>
		[Required(AllowEmptyStrings = false, ErrorMessage = "Начальная дата должна быть заполнена.")]
		public DateTime StartDate { get; set; }

		/// <summary>
		/// Конечная дата.
		/// </summary>
		[Required(AllowEmptyStrings = false, ErrorMessage = "Конечная дата должна быть заполнена.")]
		public DateTime EndDate { get; set; }
	}
}
