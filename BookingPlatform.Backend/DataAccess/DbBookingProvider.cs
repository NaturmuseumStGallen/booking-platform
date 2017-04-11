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
using BookingPlatform.Backend.Entities;

namespace BookingPlatform.Backend.DataAccess
{
	internal class DbBookingProvider : IBookingProvider
	{
		public IList<Booking> GetBookings(DateTime from, DateTime to)
		{
			var bookings = new List<Booking>();

			// TODO
			var random = new Random();

			foreach (var id in Enumerable.Range(1, 100))
			{
				bookings.Add(new Booking
				{
					Id = id,
					Date = new DateTime(from.Year, from.Month, random.Next(1, 28), random.Next(0, 23), random.Next(0, 59), 0)
				});
			}

			return bookings;
		}
	}
}
