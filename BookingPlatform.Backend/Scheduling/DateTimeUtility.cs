/*
 * Copyright (C) 2017 Naturmuseum St. Gallen
 *  > https://github.com/NaturmuseumStGallen
 *
 * Designed and engineered by Phantasus Software Systems
 *  > http://www.phantasus.ch
 *
 * This file is part of BookingPlatform.
 *
 * BookingPlatform is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * BookingPlatform is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with BookingPlatform. If not, see <http://www.gnu.org/licenses/>.
 */

using System;

namespace BookingPlatform.Backend.Scheduling
{
	public static class DateTimeUtility
	{
		/// <summary>
		/// Returns the first date of the specified month;
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public static DateTime GetFirstDayOfMonth(DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1);
		}

		/// <summary>
		/// Returns the last date of the specified month.
		/// </summary>
		public static DateTime GetLastDayOfMonth(DateTime date)
		{
			var day = DateTime.DaysInMonth(date.Year, date.Month);

			return new DateTime(date.Year, date.Month, day);
		}

		/// <summary>
		/// Retrieves the Monday of the week to which the specified date belongs to.
		/// </summary>
		public static DateTime GetMondayOfWeekFor(DateTime date)
		{
			var monday = new DateTime(date.Year, date.Month, date.Day);

			while (monday.DayOfWeek != DayOfWeek.Monday)
			{
				monday = monday.AddDays(-1);
			}

			return monday;
		}

		/// <summary>
		/// Retrieves the Sunday of the week to which the specified date belongs to.
		/// </summary>
		public static DateTime GetSundayOfWeekFor(DateTime date)
		{
			var sunday = new DateTime(date.Year, date.Month, date.Day);

			while (sunday.DayOfWeek != DayOfWeek.Sunday)
			{
				sunday = sunday.AddDays(1);
			}

			return sunday;
		}

		/// <summary>
		/// Parses the given string to a nullable <c>DateTime</c> struct. If the specified
		/// string is null, empty or whitespace, <c>null</c> will be returned.
		/// </summary>
		public static DateTime? NullableDateTimeFor(string date)
		{
			return String.IsNullOrWhiteSpace(date) ? null : (DateTime?) DateTime.Parse(date);
		}

		/// <summary>
		/// Creates a new <c>DateTime</c> struct initialized with the given date and time values.
		/// If the time value is <c>null</c>, the time of day is set to <c>00:00:00</c>.
		/// </summary>
		public static DateTime NewFor(DateTime date, TimeSpan? time)
		{
			var hour = time.HasValue ? time.Value.Hours : 0;
			var minute = time.HasValue ? time.Value.Minutes : 0;

			return new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
		}

		/// <summary>
		/// Creates a new <c>DateTime</c> struct initialized with the given number of ticks.
		/// The seconds will be ignored and set to 0.
		/// </summary>
		public static DateTime NewFor(long ticks)
		{
			var dateTime = new DateTime(ticks);

			return NewFor(dateTime, dateTime.TimeOfDay);
		}

		/// <summary>
		/// Parses the given string to a nullable <c>DateTime</c> struct. If the specified
		/// string is null, empty or whitespace, <c>null</c> will be returned.
		/// </summary>
		public static TimeSpan? NullableTimeSpanFor(string time)
		{
			return String.IsNullOrWhiteSpace(time) ? null : (TimeSpan?) TimeSpan.Parse(time);
		}

		/// <summary>
		/// Returns the given nullable <c>DateTime</c> formatted according to the specified format.
		/// If the <c>DateTime</c>'s value is null, null will be returned.
		/// </summary>
		public static string ToString(this DateTime? date, string format)
		{
			return date.HasValue ? date.Value.ToString(format) : null;
		}

		/// <summary>
		/// Returns the given nullable <c>TimeSpan</c> formatted according to the specified format.
		/// If the <c>TimeSpan</c>'s value is null, null will be returned.
		/// </summary>
		public static string ToString(this TimeSpan? time, string format)
		{
			return time.HasValue ? time.Value.ToString(format) : null;
		}
	}
}
