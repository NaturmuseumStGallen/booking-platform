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
using BookingPlatform.Backend.Scheduling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookingPlatform.Tests
{
	[TestClass]
	public class SchedulerTests
	{
		[TestMethod]
		public void MustNotReturnNull()
		{
			var bookings = new Mock<IBookingProvider>();
			var rules = new Mock<IRuleProvider>();
			var times = new Mock<ITimeProvider>();
			var @event = new Event();
			var provider = new Scheduler(bookings.Object, rules.Object, times.Object);

			bookings.Setup(p => p.GetBookings(DateTime.Now, DateTime.Now)).Returns(new List<Booking>());
			rules.Setup(r => r.GetRules()).Returns(new List<IRule>());
			times.Setup(t => t.GetTimes()).Returns(new List<TimeSpan>());

			var dates = provider.GetBookingDateRange(DateTime.Today, DateTime.Today.AddDays(1), @event);

			Assert.IsNotNull(dates);
		}

		[TestMethod]
		public void MustReturnEmptyList()
		{
			var bookings = new Mock<IBookingProvider>();
			var rules = new Mock<IRuleProvider>();
			var times = new Mock<ITimeProvider>();
			var @event = new Event();
			var provider = new Scheduler(bookings.Object, rules.Object, times.Object);

			bookings.Setup(p => p.GetBookings(DateTime.Now, DateTime.Now)).Returns(new List<Booking>());
			rules.Setup(r => r.GetRules()).Returns(new List<IRule>());
			times.Setup(t => t.GetTimes()).Returns(new List<TimeSpan>());

			var dates = provider.GetBookingDateRange(DateTime.Today, DateTime.Today.AddDays(1), @event);

			Assert.IsTrue(dates.Count == 0);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void FromMustBeSmallerThanTo()
		{
			var bookings = new Mock<IBookingProvider>();
			var rules = new Mock<IRuleProvider>();
			var times = new Mock<ITimeProvider>();
			var @event = new Event();
			var from = DateTime.Today.AddDays(1);
			var to = DateTime.Today;
			var provider = new Scheduler(bookings.Object, rules.Object, times.Object);

			bookings.Setup(p => p.GetBookings(from, to)).Returns(new List<Booking>());
			rules.Setup(r => r.GetRules()).Returns(new List<IRule>());
			times.Setup(t => t.GetTimes()).Returns(new List<TimeSpan>());

			provider.GetBookingDateRange(from, to, @event);
		}

		[TestMethod]
		public void FromCanBeSameAsTo()
		{
			var bookings = new Mock<IBookingProvider>();
			var rules = new Mock<IRuleProvider>();
			var times = new Mock<ITimeProvider>();
			var @event = new Event();
			var from = DateTime.Today;
			var to = from;
			var provider = new Scheduler(bookings.Object, rules.Object, times.Object);

			bookings.Setup(p => p.GetBookings(from, to)).Returns(new List<Booking>());
			rules.Setup(r => r.GetRules()).Returns(new List<IRule>());
			times.Setup(t => t.GetTimes()).Returns(new List<TimeSpan> { new TimeSpan(10, 0, 0) });

			var dates = provider.GetBookingDateRange(from, to, @event);

			Assert.IsTrue(dates.Count == 1);
			Assert.IsTrue(dates.First().Date == new DateTime(from.Year, from.Month, from.Day, 10, 0, 0));
		}

		[TestMethod]
		public void MustIncludeFromAndToDates()
		{
			var bookings = new Mock<IBookingProvider>();
			var rules = new Mock<IRuleProvider>();
			var times = new Mock<ITimeProvider>();
			var @event = new Event();
			var from = DateTime.Today;
			var to = DateTime.Today.AddDays(2);
			var provider = new Scheduler(bookings.Object, rules.Object, times.Object);

			bookings.Setup(p => p.GetBookings(from, to)).Returns(new List<Booking>());
			rules.Setup(r => r.GetRules()).Returns(new List<IRule>());
			times.Setup(t => t.GetTimes()).Returns(new List<TimeSpan> { new TimeSpan(10, 0, 0) });

			var dates = provider.GetBookingDateRange(from, to, @event);

			Assert.IsTrue(dates.First().Date.Date == from.Date);
			Assert.IsTrue(dates.Last().Date.Date == to.Date);
		}

		[TestMethod]
		public void ScheduleMustBeAccordingToTimeProvider()
		{
			var bookings = new Mock<IBookingProvider>();
			var rules = new Mock<IRuleProvider>();
			var times = new Mock<ITimeProvider>();
			var @event = new Event();
			var from = DateTime.Today;
			var to = DateTime.Today.AddDays(9);
			var time = new TimeSpan(10, 0, 0);
			var provider = new Scheduler(bookings.Object, rules.Object, times.Object);

			bookings.Setup(p => p.GetBookings(from, to)).Returns(new List<Booking>());
			rules.Setup(r => r.GetRules()).Returns(new List<IRule>());
			times.Setup(t => t.GetTimes()).Returns(new List<TimeSpan> { time });

			var dates = provider.GetBookingDateRange(from, to, @event);

			Assert.IsTrue(dates.Count == 10);
			Assert.IsTrue(dates.All(d => d.Date.TimeOfDay == time));
		}

		[TestMethod]
		public void DefaultStatusMustBeFree()
		{
			var bookings = new Mock<IBookingProvider>();
			var rules = new Mock<IRuleProvider>();
			var times = new Mock<ITimeProvider>();
			var @event = new Event();
			var from = DateTime.Today;
			var to = DateTime.Today.AddDays(9);
			var time = new TimeSpan(10, 0, 0);
			var provider = new Scheduler(bookings.Object, rules.Object, times.Object);

			bookings.Setup(p => p.GetBookings(from, to)).Returns(new List<Booking>());
			rules.Setup(r => r.GetRules()).Returns(new List<IRule>());
			times.Setup(t => t.GetTimes()).Returns(new List<TimeSpan> { time });

			var dates = provider.GetBookingDateRange(from, to, @event);

			Assert.IsTrue(dates.All(d => d.Status == AvailabilityStatus.Free));
		}

		[TestMethod]
		public void MustRespectAlreadyBookedEvents()
		{
			var bookings = new Mock<IBookingProvider>();
			var rules = new Mock<IRuleProvider>();
			var times = new Mock<ITimeProvider>();
			var @event = new Event { Id = 3 };
			var date = new DateTime(2017, 1, 1, 10, 0, 0);
			var provider = new Scheduler(bookings.Object, rules.Object, times.Object);

			bookings.Setup(p => p.GetBookings(date, date)).Returns(new List<Booking> { new Booking { IsActive = true, Date = date, Event = @event } });
			rules.Setup(r => r.GetRules()).Returns(new List<IRule>());
			times.Setup(t => t.GetTimes()).Returns(new List<TimeSpan> { date.TimeOfDay });

			var dates = provider.GetBookingDateRange(date, date, @event);

			Assert.IsTrue(dates.First().Status == AvailabilityStatus.Booked);
		}

		[TestMethod]
		public void MustRespectConfiguredRules()
		{
			var bookings = new Mock<IBookingProvider>();
			var rule = new Mock<IRule>();
			var rules = new Mock<IRuleProvider>();
			var times = new Mock<ITimeProvider>();
			var @event = new Event();
			var date = new DateTime(2017, 1, 1, 10, 0, 0);
			var provider = new Scheduler(bookings.Object, rules.Object, times.Object);

			bookings.Setup(p => p.GetBookings(date, date)).Returns(new List<Booking>());
			rule.Setup(r => r.GetStatus(date, @event)).Returns(AvailabilityStatus.Booked);
			rules.Setup(r => r.GetRules()).Returns(new List<IRule> { rule.Object });
			times.Setup(t => t.GetTimes()).Returns(new List<TimeSpan> { date.TimeOfDay });

			var dates = provider.GetBookingDateRange(date, date, @event);

			Assert.IsTrue(dates.First().Status == AvailabilityStatus.Booked);
		}

		[TestMethod]
		public void MustRespectStrongestRuleStatus()
		{
			var bookings = new Mock<IBookingProvider>();
			var bookedRule = new Mock<IRule>();
			var freeRule = new Mock<IRule>();
			var notBookableRule = new Mock<IRule>();
			var rules = new Mock<IRuleProvider>();
			var times = new Mock<ITimeProvider>();
			var @event = new Event();
			var date = new DateTime(2017, 1, 1, 10, 0, 0);
			var provider = new Scheduler(bookings.Object, rules.Object, times.Object);

			bookings.Setup(p => p.GetBookings(date, date)).Returns(new List<Booking>());
			bookedRule.Setup(r => r.GetStatus(date, @event)).Returns(AvailabilityStatus.Booked);
			freeRule.Setup(r => r.GetStatus(date, @event)).Returns(AvailabilityStatus.Free);
			notBookableRule.Setup(r => r.GetStatus(date, @event)).Returns(AvailabilityStatus.NotBookable);
			rules.Setup(r => r.GetRules()).Returns(new List<IRule> { bookedRule.Object, freeRule.Object, notBookableRule.Object });
			times.Setup(t => t.GetTimes()).Returns(new List<TimeSpan> { date.TimeOfDay });

			var dates = provider.GetBookingDateRange(date, date, @event);

			Assert.IsTrue(dates.First().Status == AvailabilityStatus.Free);
		}

		[TestMethod]
		public void MustRespectSecondStrongestRuleStatus()
		{
			var bookings = new Mock<IBookingProvider>();
			var bookedRule = new Mock<IRule>();
			var notBookableRule = new Mock<IRule>();
			var rules = new Mock<IRuleProvider>();
			var times = new Mock<ITimeProvider>();
			var @event = new Event();
			var date = new DateTime(2017, 1, 1, 10, 0, 0);
			var provider = new Scheduler(bookings.Object, rules.Object, times.Object);

			bookings.Setup(p => p.GetBookings(date, date)).Returns(new List<Booking>());
			bookedRule.Setup(r => r.GetStatus(date, @event)).Returns(AvailabilityStatus.Booked);
			notBookableRule.Setup(r => r.GetStatus(date, @event)).Returns(AvailabilityStatus.NotBookable);
			rules.Setup(r => r.GetRules()).Returns(new List<IRule> { bookedRule.Object, notBookableRule.Object });
			times.Setup(t => t.GetTimes()).Returns(new List<TimeSpan> { date.TimeOfDay });

			var dates = provider.GetBookingDateRange(date, date, @event);

			Assert.IsTrue(dates.First().Status == AvailabilityStatus.NotBookable);
		}
	}
}
