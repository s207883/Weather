using System;

namespace Weather.Core.Structs
{
	/// <summary>
	/// Временной промежуток.
	/// </summary>
	public struct TimeRange
	{
		/// <summary>
		/// Временной промежуток.
		/// </summary>
		/// <param name="startDate">Начало.</param>
		/// <param name="endDate">Конец.</param>
		public TimeRange(DateTime startDate, DateTime endDate)
		{
			StartDate = startDate;
			EndDate = endDate;
		}

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
