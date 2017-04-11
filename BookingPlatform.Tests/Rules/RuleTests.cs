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
using BookingPlatform.Backend.Entities;
using BookingPlatform.Backend.Rules;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookingPlatform.Tests
{
	[TestClass]
	public class RuleTests
	{
		[TestMethod]
		public void DateRangeRuleTest()
		{
			var rule = new DateRangeRule(DateTime.Today, DateTime.Today.AddDays(7), AvailabilityStatus.NotBookable);

			Assert.IsTrue(rule.GetStatus(DateTime.Today.AddDays(-1), null) == AvailabilityStatus.Undefined);
			Assert.IsTrue(rule.GetStatus(DateTime.Today, null) == AvailabilityStatus.NotBookable);
			Assert.IsTrue(rule.GetStatus(DateTime.Today.AddDays(3), null) == AvailabilityStatus.NotBookable);
			Assert.IsTrue(rule.GetStatus(DateTime.Today.AddMonths(1), null) == AvailabilityStatus.Undefined);
		}

		[TestMethod]
		public void WeeklyRuleWholeDayTest()
		{
			var rule = new WeeklyRule(DayOfWeek.Wednesday, AvailabilityStatus.Booked);

			Assert.IsTrue(rule.GetStatus(new DateTime(2016, 10, 31), null) == AvailabilityStatus.Undefined);
			Assert.IsTrue(rule.GetStatus(new DateTime(2016, 11, 2), null) == AvailabilityStatus.Booked);
			Assert.IsTrue(rule.GetStatus(new DateTime(2016, 11, 3), null) == AvailabilityStatus.Undefined);
			Assert.IsTrue(rule.GetStatus(new DateTime(2016, 11, 4), null) == AvailabilityStatus.Undefined);
			Assert.IsTrue(rule.GetStatus(new DateTime(2016, 11, 5), null) == AvailabilityStatus.Undefined);
			Assert.IsTrue(rule.GetStatus(new DateTime(2016, 11, 6), null) == AvailabilityStatus.Undefined);
		}

		[TestMethod]
		public void WeeklyRuleTimeTest()
		{
			var rule = new WeeklyRule(DayOfWeek.Thursday, AvailabilityStatus.Free, new TimeSpan(10, 30, 0));

			Assert.IsTrue(rule.GetStatus(new DateTime(2017, 2, 2, 10, 0, 0), null) == AvailabilityStatus.Undefined);
			Assert.IsTrue(rule.GetStatus(new DateTime(2017, 2, 2, 10, 29, 0), null) == AvailabilityStatus.Undefined);
			Assert.IsTrue(rule.GetStatus(new DateTime(2017, 2, 2, 10, 30, 0), null) == AvailabilityStatus.Free);
			Assert.IsTrue(rule.GetStatus(new DateTime(2017, 2, 2, 10, 31, 0), null) == AvailabilityStatus.Undefined);
			Assert.IsTrue(rule.GetStatus(new DateTime(2017, 2, 2, 20, 30, 0), null) == AvailabilityStatus.Undefined);
			Assert.IsTrue(rule.GetStatus(new DateTime(2017, 3, 2, 10, 30, 0), null) == AvailabilityStatus.Free);
		}

		[TestMethod]
		public void WeeklyRuleStartDateTest()
		{
			var rule = new WeeklyRule(DayOfWeek.Tuesday, AvailabilityStatus.NotBookable, startDate: new DateTime(2017, 3, 14));

			Assert.IsTrue(rule.GetStatus(new DateTime(2017, 3, 7), null) == AvailabilityStatus.Undefined);
			Assert.IsTrue(rule.GetStatus(new DateTime(2017, 3, 14), null) == AvailabilityStatus.NotBookable);
			Assert.IsTrue(rule.GetStatus(new DateTime(2017, 3, 21), null) == AvailabilityStatus.NotBookable);
			Assert.IsTrue(rule.GetStatus(new DateTime(2018, 3, 20), null) == AvailabilityStatus.NotBookable);
			Assert.IsTrue(rule.GetStatus(new DateTime(2018, 3, 21), null) == AvailabilityStatus.Undefined);
		}

		[TestMethod]
		public void EventGroupRuleTest()
		{
			var date = new DateTime(2017, 4, 1, 12, 30, 0);
			var eventGroup = new EventGroup
			{
				Bookings = new List<Booking> { new Booking { EventId = 4, Date = date } },
				Events = new List<Event> { new Event { Id = 2 }, new Event { Id = 4 }, new Event { Id = 5 } }
			};
			var rule = new EventGroupRule(eventGroup, AvailabilityStatus.Booked);

			Assert.IsTrue(rule.GetStatus(date, new Event { Id = 2 }) == AvailabilityStatus.Booked);
			Assert.IsTrue(rule.GetStatus(date, new Event { Id = 4 }) == AvailabilityStatus.Booked);
			Assert.IsTrue(rule.GetStatus(date, new Event { Id = 5 }) == AvailabilityStatus.Booked);

			Assert.IsTrue(rule.GetStatus(date, new Event { Id = 6 }) == AvailabilityStatus.Undefined);
			Assert.IsTrue(rule.GetStatus(new DateTime(2017, 4, 1, 12, 0, 0), new Event { Id = 2 }) == AvailabilityStatus.Undefined);
			Assert.IsTrue(rule.GetStatus(new DateTime(2017, 4, 1, 12, 0, 0), new Event { Id = 4 }) == AvailabilityStatus.Undefined);
			Assert.IsTrue(rule.GetStatus(new DateTime(2017, 4, 1, 12, 0, 0), new Event { Id = 5 }) == AvailabilityStatus.Undefined);
		}
	}
}
