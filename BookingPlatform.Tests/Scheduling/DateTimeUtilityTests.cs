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
using BookingPlatform.Backend.Scheduling;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookingPlatform.Tests
{
	[TestClass]
	public class DateTimeUtilityTests
	{
		[TestMethod]
		public void FirstDayOfMonthTest()
		{
			var first = DateTimeUtility.GetFirstDayOfMonth(new DateTime(2017, 4, 5));

			Assert.IsTrue(first.DayOfWeek == DayOfWeek.Saturday);
			Assert.IsTrue(first.Year == 2017);
			Assert.IsTrue(first.Month == 4);
			Assert.IsTrue(first.Day == 1);
			Assert.IsTrue(first.Hour == 0);
			Assert.IsTrue(first.Minute == 0);
			Assert.IsTrue(first.Second == 0);
		}

		[TestMethod]
		public void LastDayOfMonthTest()
		{
			var last = DateTimeUtility.GetLastDayOfMonth(new DateTime(2017, 4, 5));

			Assert.IsTrue(last.DayOfWeek == DayOfWeek.Sunday);
			Assert.IsTrue(last.Year == 2017);
			Assert.IsTrue(last.Month == 4);
			Assert.IsTrue(last.Day == 30);
			Assert.IsTrue(last.Hour == 0);
			Assert.IsTrue(last.Minute == 0);
			Assert.IsTrue(last.Second == 0);
		}

		[TestMethod]
		public void MondayOfWeekTest()
		{
			var monday = DateTimeUtility.GetMondayOfWeekFor(new DateTime(2017, 2, 8));

			Assert.IsTrue(monday.DayOfWeek == DayOfWeek.Monday);
			Assert.IsTrue(monday.Year == 2017);
			Assert.IsTrue(monday.Month == 2);
			Assert.IsTrue(monday.Day == 6);
			Assert.IsTrue(monday.Hour == 0);
			Assert.IsTrue(monday.Minute == 0);
			Assert.IsTrue(monday.Second == 0);
		}

		[TestMethod]
		public void SundayOfWeekTest()
		{
			var sunday = DateTimeUtility.GetSundayOfWeekFor(new DateTime(2017, 2, 8));

			Assert.IsTrue(sunday.DayOfWeek == DayOfWeek.Sunday);
			Assert.IsTrue(sunday.Year == 2017);
			Assert.IsTrue(sunday.Month == 2);
			Assert.IsTrue(sunday.Day == 12);
			Assert.IsTrue(sunday.Hour == 0);
			Assert.IsTrue(sunday.Minute == 0);
			Assert.IsTrue(sunday.Second == 0);
		}

		[TestMethod]
		public void NewForDateOnlyTest()
		{
			var date = new DateTime(2017, 3, 12);
			var @new = DateTimeUtility.NewFor(date, null);

			Assert.IsTrue(@new.DayOfWeek == DayOfWeek.Sunday);
			Assert.IsTrue(@new.Year == 2017);
			Assert.IsTrue(@new.Month == 3);
			Assert.IsTrue(@new.Day == 12);
			Assert.IsTrue(@new.Hour == 0);
			Assert.IsTrue(@new.Minute == 0);
			Assert.IsTrue(@new.Second == 0);
		}

		[TestMethod]
		public void NewForDateAndTimeTest()
		{
			var date = new DateTime(2017, 3, 14, 11, 45, 10);
			var time = new TimeSpan(10, 30, 25);
			var @new = DateTimeUtility.NewFor(date, time);

			Assert.IsTrue(@new.DayOfWeek == DayOfWeek.Tuesday);
			Assert.IsTrue(@new.Year == 2017);
			Assert.IsTrue(@new.Month == 3);
			Assert.IsTrue(@new.Day == 14);
			Assert.IsTrue(@new.Hour == 10);
			Assert.IsTrue(@new.Minute == 30);
			Assert.IsTrue(@new.Second == 0);
		}

		[TestMethod]
		public void NewForTicksTimeTest()
		{
			var @new = DateTimeUtility.NewFor(15614613513553);

			Assert.IsTrue(@new.Second == 0);
			Assert.IsTrue(@new.Millisecond == 0);
		}
	}
}
