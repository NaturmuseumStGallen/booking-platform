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
using BookingPlatform.Backend.Rules;

namespace BookingPlatform.Backend.Scheduling
{
	public class Scheduler
	{
		private IBookingProvider bookingProvider;
		private IRuleProvider ruleProvider;
		private ITimeProvider timeProvider;

		private IList<Entities.Booking> bookings;
		private IList<IRule> rules;
		private IList<TimeSpan> times;

		public Scheduler(IBookingProvider bookingProvider, IRuleProvider ruleProvider, ITimeProvider timeProvider)
		{
			this.bookingProvider = bookingProvider;
			this.ruleProvider = ruleProvider;
			this.timeProvider = timeProvider;
		}

		/// <summary>
		/// Returns the range of booking dates for the specified time period and event. From and to are inclusive,
		/// i.e. they can be the same to get the schedule for a single day.
		/// </summary>
		/// <param name="from">The start date of the time period.</param>
		/// <param name="to">The end date of the time period.</param>
		/// <param name="event">The event for which to determine the available schedule.</param>
		/// <returns>A list of booking dates.</returns>
		public IList<BookingDate> GetBookingDateRange(DateTime from, DateTime to, Event @event)
		{
			var dates = new List<BookingDate>();
			var day = new DateTime(from.Ticks);

			CheckIfDatesValid(from, to);
			InitializeProviders(from, to);

			while (day <= to)
			{
				foreach (var time in times)
				{
					var availability = new BookingDate();

					availability.Date = new DateTime(day.Year, day.Month, day.Day, time.Hours, time.Minutes, 0);
					availability.Status = DetermineStatus(day, time, @event);

					dates.Add(availability);
				}

				day = day.AddDays(1);
			}

			return dates;
		}

		private void CheckIfDatesValid(DateTime from, DateTime to)
		{
			if (from.Date > to.Date)
			{
				throw new ArgumentException(nameof(from) + " must be smaller than " + nameof(to));
			}
		}

		private void InitializeProviders(DateTime from, DateTime to)
		{
			bookings = bookingProvider.GetBookings(from, to);
			rules = ruleProvider.GetRules(from, to);
			times = timeProvider.GetTimes();
		}

		private AvailabilityStatus DetermineStatus(DateTime date, TimeSpan time, Event @event)
		{
			var dateTime = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, 0);
			var ruleResults = new List<AvailabilityStatus>();

			foreach (var booking in bookings)
			{
				if (booking.EventId == @event.Id && booking.Date.IsSameDateAndTimeAs(dateTime))
				{
					return AvailabilityStatus.Booked;
				}
			}

			foreach (var rule in rules)
			{
				var status = rule.GetStatus(dateTime, @event);

				if (status != AvailabilityStatus.Undefined)
				{
					ruleResults.Add(status);
				}
			}

			if (ruleResults.Count > 0)
			{
				return GetStrongestStatus(ruleResults);
			}

			return AvailabilityStatus.Free;
		}

		private AvailabilityStatus GetStrongestStatus(IList<AvailabilityStatus> states)
		{
			if (states.Any(s => s == AvailabilityStatus.Free))
			{
				return AvailabilityStatus.Free;
			}

			if (states.Any(s => s == AvailabilityStatus.NotBookable))
			{
				return AvailabilityStatus.NotBookable;
			}

			return AvailabilityStatus.Booked;
		}
	}
}
