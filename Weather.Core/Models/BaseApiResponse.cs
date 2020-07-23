namespace Weather.Core.Models
{
	/// <summary>
	/// Базовый ответ API.
	/// </summary>
	/// <typeparam name="T">Модель возвращаемых данных.</typeparam>
	public class BaseApiResponse<T> : BaseApiResponse where T : class
	{
		/// <summary>
		/// Данные.
		/// </summary>
		public T Data { get; set; }
	}

	/// <summary>
	/// Базовый ответ API.
	/// </summary>
	public class BaseApiResponse
	{
		/// <summary>
		/// Успешность запроса.
		/// </summary>
		public bool IsSuccess { get; set; }

		/// <summary>
		/// Сообщение.
		/// </summary>
		public string Message { get; set; }
	}
}
