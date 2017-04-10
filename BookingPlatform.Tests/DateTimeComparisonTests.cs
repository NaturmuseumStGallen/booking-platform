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
using BookingPlatform.Backend.Scheduling;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookingPlatform.Tests
{
	[TestClass]
	public class DateTimeComparisonTests
	{
		[TestMethod]
		public void IsSameDateAsTest()
		{
			Assert.IsTrue(new DateTime(2017, 1, 1).IsSameDateAs(new DateTime(2017, 1, 1)));
			Assert.IsTrue(new DateTime(2017, 1, 1).IsSameDateAs(new DateTime(2017, 1, 1, 10, 30, 45)));
			Assert.IsTrue(new DateTime(2017, 1, 1, 9, 45, 0).IsSameDateAs(new DateTime(2017, 1, 1)));

			Assert.IsFalse(new DateTime(2016, 12, 31).IsSameDateAs(new DateTime(2017, 1, 2)));
			Assert.IsFalse(new DateTime(2017, 1, 1, 23, 59, 59).IsSameDateAs(new DateTime(2017, 1, 2)));
			Assert.IsFalse(new DateTime(2017, 1, 1).IsSameDateAs(new DateTime(2017, 1, 2)));
		}

		[TestMethod]
		public void IsSameDateAndTimeTest()
		{
			Assert.IsTrue(new DateTime(2017, 1, 1).IsSameDateAndTimeAs(new DateTime(2017, 1, 1)));
			Assert.IsTrue(new DateTime(2017, 1, 1, 12, 0, 0).IsSameDateAndTimeAs(new DateTime(2017, 1, 1, 12, 0, 0)));
			Assert.IsTrue(new DateTime(2017, 1, 1, 9, 45, 0).IsSameDateAndTimeAs(new DateTime(2017, 1, 1, 9, 45, 15)));

			Assert.IsFalse(new DateTime(2017, 1, 1).IsSameDateAndTimeAs(new DateTime(2017, 1, 1, 23, 59, 59)));
			Assert.IsFalse(new DateTime(2017, 1, 1, 23, 59, 59).IsSameDateAndTimeAs(new DateTime(2017, 1, 2)));
			Assert.IsFalse(new DateTime(2017, 1, 1, 23, 59, 59).IsSameDateAndTimeAs(new DateTime(2017, 1, 1, 23, 58, 59)));
			Assert.IsFalse(new DateTime(2016, 1, 1, 23, 59, 59).IsSameDateAndTimeAs(new DateTime(2017, 1, 1, 23, 59, 59)));
		}

		[TestMethod]
		public void IsBiggerThanTest()
		{
			Assert.IsTrue(new DateTime(2017, 1, 1).IsBiggerThan(new DateTime(2016, 12, 31)));
			Assert.IsTrue(new DateTime(2017, 1, 1, 12, 1, 0).IsBiggerThan(new DateTime(2017, 1, 1, 12, 0, 0)));

			Assert.IsFalse(new DateTime(2017, 1, 1, 12, 0, 0).IsBiggerThan(new DateTime(2017, 1, 1, 12, 0, 10)));
			Assert.IsFalse(new DateTime(2016, 1, 1, 12, 0, 0).IsBiggerThan(new DateTime(2017, 1, 1, 12, 0, 10)));
		}

		[TestMethod]
		public void IsBiggerThanOrEqualAsTest()
		{
			Assert.IsTrue(new DateTime(2017, 1, 1).IsBiggerThanOrEqualAs(new DateTime(2016, 12, 31)));
			Assert.IsTrue(new DateTime(2017, 1, 1, 12, 1, 0).IsBiggerThanOrEqualAs(new DateTime(2017, 1, 1, 12, 0, 0)));
			Assert.IsTrue(new DateTime(2017, 1, 1, 12, 0, 0).IsBiggerThanOrEqualAs(new DateTime(2017, 1, 1, 12, 0, 0)));
			Assert.IsTrue(new DateTime(2017, 1, 1, 12, 0, 0).IsBiggerThanOrEqualAs(new DateTime(2017, 1, 1, 12, 0, 10)));

			Assert.IsFalse(new DateTime(2016, 1, 1, 12, 0, 0).IsBiggerThanOrEqualAs(new DateTime(2017, 1, 1, 12, 0, 10)));
		}

		[TestMethod]
		public void IsSmallerThanTest()
		{
			Assert.IsTrue(new DateTime(2016, 12, 31).IsSmallerThan(new DateTime(2017, 1, 1)));
			Assert.IsTrue(new DateTime(2017, 1, 1, 12, 0, 0).IsSmallerThan(new DateTime(2017, 1, 1, 12, 1, 0)));

			Assert.IsFalse(new DateTime(2017, 1, 1, 12, 0, 10).IsSmallerThan(new DateTime(2017, 1, 1, 12, 0, 0)));
			Assert.IsFalse(new DateTime(2017, 1, 1, 12, 0, 10).IsSmallerThan(new DateTime(2016, 1, 1, 12, 0, 0)));
		}

		[TestMethod]
		public void IsSmallerThanOrEqualAsTest()
		{
			Assert.IsTrue(new DateTime(2016, 12, 31).IsSmallerThanOrEqualAs(new DateTime(2017, 1, 1)));
			Assert.IsTrue(new DateTime(2017, 1, 1, 12, 0, 0).IsSmallerThanOrEqualAs(new DateTime(2017, 1, 1, 12, 1, 0)));
			Assert.IsTrue(new DateTime(2017, 1, 1, 12, 0, 0).IsSmallerThanOrEqualAs(new DateTime(2017, 1, 1, 12, 0, 0)));
			Assert.IsTrue(new DateTime(2017, 1, 1, 12, 0, 10).IsSmallerThanOrEqualAs(new DateTime(2017, 1, 1, 12, 0, 0)));

			Assert.IsFalse(new DateTime(2017, 1, 1, 12, 0, 10).IsSmallerThanOrEqualAs(new DateTime(2016, 1, 1, 12, 0, 0)));
		}
	}
}
