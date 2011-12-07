using System;
using System.Collections.Generic;

namespace Gecko.Extensions.DateTimeExtensions
{
	public enum AddType
	{
		Years,
		Months,
		Days,
		Hours,
		Minutes,
		Seconds,
		Milliseconds,
		Ticks
	}

	public static class Extensions
	{
		/// <summary>
		/// Calculates the average of a list of DateTimes
		/// </summary>
		/// <param name="dates">List of DateTimes of which compute the average rate</param>
		/// <returns>Average DateTime object</returns>
		public static DateTime Average(this List<DateTime> dates)
		{
			//Total number of Ticks
			long totalTicks = 0;
			//Average number of Ticks
			long averageTicks = 0;
			//Gets the total of the List of DateTime Object Ticks.
			for (int i = 0; i < dates.Count; i++)
			{
				totalTicks += dates[i].Ticks;
			}
			//Gets the average of Ticks (Average is the total divided by the number :))
			averageTicks = totalTicks / dates.Count;
			//Initializes new DateTime Object by the AverageTick object.
			return new DateTime(averageTicks);
		}

		/// <summary>
		/// Calculates average time between two dates. This Overload spans 15 minutes a date
		/// </summary>
		/// <param name="now"><seealso cref="System.DateTime"/></param>
		/// <param name="span"><seealso cref="System.DateTime"/>, end of span time</param>
		/// <returns>Average DateTime object</returns>
		public static DateTime Average(this DateTime now, DateTime span)
		{
			return now.Average(span, AddType.Minutes, 15D);
		}

		/// <summary>
		/// Calculates average time between two dates
		/// </summary>
		/// <param name="now"><seealso cref="System.DateTime"/></param>
		/// <param name="span"><seealso cref="System.DateTime"/>, end of span time</param>
		/// <param name="adder">Type structure that acts as a switcher for what type of add to perform</param>
		/// <param name="much">How much AddType to add to each element for creating list of data</param>
		/// <returns>Average DateTime object</returns>
		public static DateTime Average(this DateTime now, DateTime span, AddType adder, double much)
		{
			return GetDateRange(now, span, adder, much).Average();
		}

		/// <summary>
		/// Gets a range of DateTimes
		/// </summary>
		/// <param name="StartingDate">Starting DateTime</param>
		/// <param name="EndingDate">Ending DateTime</param>
		/// <param name="add">Type structure that acts as a switcher for what type of add to perform</param>
		/// <param name="much">How much AddType to add to each element for creating list of data</param>
		/// <returns>A list of DateTime objects that corresponds in the interval between StartingDate and EndingDate</returns>
		public static List<DateTime> GetDateRange(this DateTime StartingDate, DateTime EndingDate, AddType add, double much)
		{
			if (StartingDate > EndingDate)
			{
				return null;
			}
			List<DateTime> rv = new List<DateTime>();
			DateTime tmpDate = StartingDate;
			do
			{
				rv.Add(tmpDate);
				tmpDate = tmpDate.Add(AddType.Minutes, much);
			} while (tmpDate <= EndingDate);
			return rv;
		}

		/// <summary>
		/// Adds a generic AddType to a DateTime object
		/// </summary>
		/// <param name="now"><seealso cref="System.DateTime"/></param>
		/// <param name="adder">Type structure that acts as a switcher for what type of add to perform</param>
		/// <param name="much">How much AddType to add to each element for creating list of data</param>
		/// <returns>A DateTime object with the added AddType amounts</returns>
		public static DateTime Add(this DateTime now, AddType adder, double much)
		{
			DateTime ret = now;
			switch (adder)
			{
				case AddType.Years:
					{
						ret = now.AddYears((int)much);
						break;
					}
				case AddType.Months:
					{
						ret = now.AddMonths((int)much);
						break;
					}
				case AddType.Days:
					{
						ret = now.AddDays(much);
						break;
					}
				case AddType.Hours:
					{
						ret = now.AddHours(much);
						break;
					}
				case AddType.Minutes:
					{
						ret = now.AddMinutes(much);
						break;
					}
				case AddType.Seconds:
					{
						ret = now.AddSeconds(much);
						break;
					}
				case AddType.Milliseconds:
					{
						ret = now.AddMilliseconds(much);
						break;
					}
				case AddType.Ticks:
					{
						ret = now.AddTicks((long)much);
						break;
					}
			}
			return ret;
		}
	}
}
