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
	}
}
