namespace Weather.Core.Structs
{
	/// <summary>
	/// Структура координат.
	/// </summary>
	public struct Coordinates
	{
		/// <summary>
		/// Координаты.
		/// </summary>
		/// <param name="latitude">Широта.</param>
		/// <param name="longitude">Долгота.</param>
		public Coordinates(float latitude, float longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

		/// <summary>
		/// Широта.
		/// </summary>
		public float Latitude { get; set; }

		/// <summary>
		/// Долгота.
		/// </summary>
		public float Longitude { get; set; }
	}
}
