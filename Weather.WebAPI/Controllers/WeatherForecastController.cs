using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Weather.Core.Interfaces;
using Weather.Core.Models;
using Weather.Core.RequestModels;
using Weather.Core.Structs;
using Weather.Core.ViewModels;

namespace Weather.WebAPI.Controllers
{
	/// <summary>
	/// Контроллер погоды.
	/// </summary>
    [ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{

		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IWeatherService _weatherService;

		public WeatherForecastController(IWeatherService weatherService, ILogger<WeatherForecastController> logger)
		{
			_weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <summary>
		/// Получить погоду по координатам.
		/// </summary>
		/// <param name="weatherRequestModel">Модель запроса.</param>
		/// <returns>Прогноз погоды</returns>
		[HttpGet("[action]")]
		public async Task<ActionResult<BaseApiResponse<WeatherViewModel>>> GetWeatherByCoordinates([FromQuery] WeatherRequestModelBase weatherRequestModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(weatherRequestModel);
			}

			var weather = await _weatherService.GetWeatherByCoordinatesAsync(new Coordinates(weatherRequestModel.Latitude, weatherRequestModel.Longitude));

			if (weather == default)
			{
				var notFoundResponse = new BaseApiResponse { IsSuccess = false, Message = "Данные о погоде не найдены." };
				return NotFound(notFoundResponse);
			}

			var response = new BaseApiResponse<WeatherViewModel> { IsSuccess = true, Data = weather };

			return Ok(response);
		}

		/// <summary>
		/// Получить погоду по координатам и временному промежутку.
		/// </summary>
		/// <param name="weatherRequestModel">Модель запроса.</param>
		/// <returns>Прогноз погоды.</returns>
		[HttpGet("[action]")]
		public async Task<ActionResult<BaseApiResponse<WeatherViewModel>>> GetHistory([FromQuery] WeatherRequestModelWithDate weatherRequestModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(weatherRequestModel);
			}

			if (DateTime.Compare(weatherRequestModel.StartDate, weatherRequestModel.EndDate) > 0)
			{
				var badResponseBody = new BaseApiResponse { IsSuccess = false, Message = "Некорректная дата." };
				return BadRequest(badResponseBody);
			}

			var weather = await _weatherService.GetWeatherByCoordinatesAsync(new Coordinates(weatherRequestModel.Latitude, weatherRequestModel.Longitude), new TimeRange(weatherRequestModel.StartDate, weatherRequestModel.EndDate));

			if (weather == default)
			{
				var notFoundResponse = new BaseApiResponse { IsSuccess = false, Message = "Данные о погоде не найдены." };
				return NotFound(notFoundResponse);
			}

			var response = new BaseApiResponse<WeatherViewModel> { IsSuccess = true, Data = weather };

			return Ok(response);
		}
	}
}
