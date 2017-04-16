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
using System.Data.SqlClient;
using System.Linq;
using BookingPlatform.Backend.Entities;

namespace BookingPlatform.Backend.DataAccess
{
	internal class DbBookingDao : DbBaseDao<Booking>, IBookingProvider
	{
		public Booking GetBy(int id)
		{
			var sql = "SELECT * FROM Booking WHERE Id = @Id";
			var param = new SqlParameter("@Id", id);

			return ExecuteSingleQuery(sql, param);
		}

		public IList<Booking> GetBookings(DateTime from, DateTime to)
		{
			var sql = "SELECT * FROM Booking WHERE @From <= Date AND Date <= @To";
			var parameters = new[]
			{
				new SqlParameter("@From", from),
				new SqlParameter("@To", to)
			};

			return ExecuteMultiQuery(sql, parameters);
		}

		public void SaveNew(Booking booking)
		{

		}

		public void Update(Booking booking)
		{

		}

		public void UpdateState(int id, bool isActive)
		{

		}

		protected override Booking MapFrom(SqlDataReader reader)
		{
			var booking = new Booking();

			booking.Id = reader.GetInt32(0);
			booking.EventId = reader.GetInt32(1);
			booking.IsActive = reader.GetBoolean(2);
			booking.Address = reader.IsDBNull(3) ? null : reader.GetString(3);
			booking.Date = reader.GetDateTime(4);
			booking.Email = reader.GetString(5);
			booking.FirstName = reader.GetString(6);
			booking.Grade = reader.GetString(7);
			booking.LastName = reader.GetString(8);
			booking.Notes = reader.IsDBNull(9) ? null : reader.GetString(9);
			booking.NumberOfKids = reader.GetInt32(10);
			booking.Phone = reader.GetString(11);
			booking.School = reader.GetString(12);
			booking.Town = reader.GetString(13);
			booking.ZipCode = reader.IsDBNull(14) ? null : (int?) reader.GetInt32(14);

			return booking;
		}
	}
}
