using System;

namespace Weather.Core.Structs
{
	/// <summary>
	/// Временной промежуток.
	/// </summary>
	public struct TimeRange
	{
		/// <summary>
		/// Начальная дата.
		/// </summary>
		public DateTime StartDate { get; set; }

		/// <summary>
		/// Конечная дата.
		/// </summary>
		public DateTime EndDate { get; set; }
	}
}
