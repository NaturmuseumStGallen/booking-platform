/*
 * Copyright (C) 2017 Naturmuseum St. Gallen
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
	public static class DateTimeComparison
	{
		/// <summary>
		/// Compares the two <c>DateTime</c> structs and returns <c>true</c>
		/// if they describe the same year, month and day.
		/// </summary>
		public static bool IsSameDateAs(this DateTime a, DateTime b)
		{
			return a.Year == b.Year && a.Month == b.Month && a.Day == b.Day;
		}

		/// <summary>
		/// Compares the two <c>DateTime</c> structs and returns <c>true</c>
		/// if they describe the same year, month, day, hour and minute.
		/// </summary>
		public static bool IsSameDateAndTimeAs(this DateTime a, DateTime b)
		{
			return a.IsSameDateAs(b) && a.Hour == b.Hour && a.Minute == b.Minute;
		}

		/// <summary>
		/// Determines whether the specified <c>DateTime</c> struct describes
		/// a smaller (i.e. previous) point in time (year, month, day, hour and minute).
		/// </summary>
		public static bool IsBiggerThan(this DateTime a, DateTime b)
		{
			return a.Year > b.Year
				|| (a.Year == b.Year && a.Month > b.Month)
				|| (a.Year == b.Year && a.Month == b.Month && a.Day > b.Day)
				|| (a.Year == b.Year && a.Month == b.Month && a.Day == b.Day && a.Hour > b.Hour)
				|| (a.Year == b.Year && a.Month == b.Month && a.Day == b.Day && a.Hour == b.Hour && a.Minute > b.Minute);
		}

		/// <summary>
		/// Determines whether the specified <c>DateTime</c> struct describes
		/// a smaller (i.e. previous) or equal point in time (year, month, day, hour and minute).
		/// </summary>
		public static bool IsBiggerThanOrEqualAs(this DateTime a, DateTime b)
		{
			return a.IsBiggerThan(b) || a.IsSameDateAndTimeAs(b);
		}

		/// <summary>
		/// Determines whether the specified <c>DateTime</c> struct describes
		/// a bigger (i.e. future) point in time (year, month, day, hour and minute).
		/// </summary>
		public static bool IsSmallerThan(this DateTime a, DateTime b)
		{
			return a.Year < b.Year
				|| (a.Year == b.Year && a.Month < b.Month)
				|| (a.Year == b.Year && a.Month == b.Month && a.Day < b.Day)
				|| (a.Year == b.Year && a.Month == b.Month && a.Day == b.Day && a.Hour < b.Hour)
				|| (a.Year == b.Year && a.Month == b.Month && a.Day == b.Day && a.Hour == b.Hour && a.Minute < b.Minute);
		}

		/// <summary>
		/// Determines whether the specified <c>DateTime</c> struct describes
		/// a bigger (i.e. future) or equal point in time (year, month, day, hour and minute).
		/// </summary>
		public static bool IsSmallerThanOrEqualAs(this DateTime a, DateTime b)
		{
			return a.IsSmallerThan(b) || a.IsSameDateAndTimeAs(b);
		}
	}
}
