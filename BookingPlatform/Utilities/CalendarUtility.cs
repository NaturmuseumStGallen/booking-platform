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
using System.Collections.Generic;
using System.Linq;
using BookingPlatform.Backend.DataAccess;
using BookingPlatform.Backend.Entities;
using BookingPlatform.Backend.Scheduling;
using BookingPlatform.Backend.Rules;

namespace BookingPlatform.Utilities
{
	public static class CalendarUtility
	{
		public static IList<BookingDate> CalculateBookingDates(DateTime date, int eventId)
		{
            Scheduler scheduler;

            var bookingTimeOverrideRules = Database.Instance.GetRules().OfType<BookingTimeOverrideRule>().Where(r => r.EventId == eventId);

            if (bookingTimeOverrideRules.Any())
            {
                var timeProvider = new BookingTimeOverrideTimeProvider(bookingTimeOverrideRules);
                scheduler = new Scheduler(Database.Instance, Database.Instance, timeProvider);
            }
            else
            {
                scheduler = Scheduler.CreateNew(Database.Instance);
            }
               
			var @event = Database.Instance.GetEventBy(eventId);
			var monday = DateTimeUtility.GetMondayOfWeekFor(date);
			var sunday = DateTimeUtility.GetSundayOfWeekFor(date);

			return scheduler.GetBookingDateRange(monday, sunday, @event);
		}

		public static DateTime CalculateFirstFreeBookingDate(int eventId)
		{
			var date = DateTime.Today;
			var searchLimit = DateTime.Today.AddMonths(1);
			var dates = CalculateBookingDates(date, eventId);

			while (dates.All(d => d.Status != AvailabilityStatus.Free))
			{
				date = date.AddDays(7);
				dates = CalculateBookingDates(date, eventId);

				if (date.IsBiggerThan(searchLimit))
				{
					break;
				}
			}

			return date;
		}

		public static DateTime CalculateNewDate(DateTime current, Navigation? navigation)
		{
			if (navigation.HasValue && Enum.IsDefined(typeof(Navigation), navigation))
			{
				switch (navigation)
				{
					case Navigation.PreviousMonth:
						return current.AddMonths(-1);
					case Navigation.PreviousWeek:
						return current.AddDays(-7);
					case Navigation.NextWeek:
						return current.AddDays(7);
					case Navigation.NextMonth:
						return current.AddMonths(1);
				}
			}

			return current;
		}

		public static bool CanNavigateToPreviousWeek(DateTime current)
		{
			var minus1Week = current.AddDays(-7);
			var sunday = DateTimeUtility.GetSundayOfWeekFor(minus1Week);

			return DateTime.Today.IsSmallerThanOrEqualAs(sunday);
		}

		public static bool CanNavigateToPreviousMonth(DateTime current)
		{
			var minus1Month = current.AddMonths(-1);
			var sunday = DateTimeUtility.GetSundayOfWeekFor(minus1Month);

			return DateTime.Today.IsSmallerThanOrEqualAs(sunday);
		}

		public enum Navigation
		{
			PreviousMonth = -2,
			PreviousWeek = -1,
			None = 0,
			NextWeek = 1,
			NextMonth = 2
		}
	}
}