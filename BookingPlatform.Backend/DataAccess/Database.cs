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

namespace BookingPlatform.Backend.DataAccess
{
	public class Database : IBookingProvider, IRuleProvider, ITimeProvider
	{
		private Database()
		{
		}

		public static Database Instance
		{
			get { return new Database(); }
		}

		public static bool IsValidEventId(int id)
		{
			// TODO!
			return id >= 0;
		}

		public IList<Booking> GetBookings(DateTime from, DateTime to)
		{
			return new DbBookingProvider().GetBookings(from, to);
		}

		public IList<IRule> GetRules(DateTime from, DateTime to)
		{
			return new DbRuleProvider().GetRules(from, to);
		}

		public IList<TimeSpan> GetTimes()
		{
			return new DbTimeProvider().GetTimes();
		}
	}
}
