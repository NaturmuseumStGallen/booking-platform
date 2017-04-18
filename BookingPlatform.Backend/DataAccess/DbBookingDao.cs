﻿/*
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
		public bool Exists(int id)
		{
			var sql = "SELECT COUNT(*) FROM Booking WHERE Id = @Id";
			var parameter = new SqlParameter("@Id", id);

			return Convert.ToInt32(ExecuteScalar(sql, parameter)) == 1;
		}

		public Booking GetBy(int id)
		{
			var sql = "SELECT * FROM Booking WHERE Id = @Id";
			var parameter = new SqlParameter("@Id", id);

			return ExecuteSingleQuery(sql, parameter);
		}

		public IList<Booking> GetByEvents(IList<int> eventIds)
		{
			var sql = "SELECT * FROM Booking WHERE IsActive = 1 AND EventId IN (%%PARAM_LIST%%)";
			var sqlParamList = String.Empty;
			var parameters = new List<SqlParameter>();
			var ids = eventIds.Distinct().ToList();

			foreach (var id in ids)
			{
				var paramId = "@EventId" + ids.IndexOf(id);

				sqlParamList += paramId + (ids.Last() == id ? string.Empty : ", ");
				parameters.Add(new SqlParameter(paramId, id));
			}

			sql = sql.Replace("%%PARAM_LIST%%", sqlParamList);

			return ExecuteMultiQuery(sql, parameters.ToArray());
		}

		public IList<Booking> GetBookings(DateTime from, DateTime to)
		{
			var sql = "SELECT * FROM Booking WHERE @From <= [Date] AND [Date] <= @To";
			var parameters = new[]
			{
				new SqlParameter("@From", from),
				new SqlParameter("@To", to)
			};

			return ExecuteMultiQuery(sql, parameters);
		}

		public void SaveNew(Booking booking)
		{
			var sql = @"
			INSERT INTO
				Booking(EventId, IsActive, [Address], [Date], Email, FirstName, Grade, LastName, Notes, NumberOfKids, Phone, School, Town, ZipCode)
			VALUES
				(@EventId, @IsActive, @Address, @Date, @Email, @FirstName, @Grade, @LastName, @Notes, @NumberOfKids, @Phone, @School, @Town, @ZipCode)";
			var parameters = new[]
			{
				new SqlParameter("@EventId", booking.EventId),
				new SqlParameter("@IsActive", booking.IsActive),
				new SqlParameter("@Address", (object) booking.Address ?? DBNull.Value),
				new SqlParameter("@Date", booking.Date),
				new SqlParameter("@Email", booking.Email),
				new SqlParameter("@FirstName", booking.FirstName),
				new SqlParameter("@Grade", booking.Grade),
				new SqlParameter("@LastName", booking.LastName),
				new SqlParameter("@Notes", (object) booking.Notes ?? DBNull.Value),
				new SqlParameter("@NumberOfKids", booking.NumberOfKids),
				new SqlParameter("@Phone", booking.Phone),
				new SqlParameter("@School", booking.School),
				new SqlParameter("@Town", booking.Town),
				new SqlParameter("@ZipCode", (object) booking.ZipCode ?? DBNull.Value)
			};

			ExecuteNonQuery(sql, parameters);
		}

		public void Update(Booking booking)
		{
			var sql = @"
			UPDATE
				Booking
			SET
				EventId = @EventId,
				IsActive = @IsActive,
				[Address] = @Address,
				[Date] = @Date,
				Email = @Email,
				FirstName = @FirstName,
				Grade = @Grade,
				LastName = @LastName,
				Notes = @Notes,
				NumberOfKids = @NumberOfKids,
				Phone = @Phone,
				School = @School,
				Town = @Town,
				ZipCode = @ZipCode
			WHERE
				Id = @Id";
			var parameters = new[]
			{
				new SqlParameter("@Id", booking.Id),
				new SqlParameter("@EventId", booking.EventId),
				new SqlParameter("@IsActive", booking.IsActive),
				new SqlParameter("@Address", (object) booking.Address ?? DBNull.Value),
				new SqlParameter("@Date", booking.Date),
				new SqlParameter("@Email", booking.Email),
				new SqlParameter("@FirstName", booking.FirstName),
				new SqlParameter("@Grade", booking.Grade),
				new SqlParameter("@LastName", booking.LastName),
				new SqlParameter("@Notes", (object) booking.Notes ?? DBNull.Value),
				new SqlParameter("@NumberOfKids", booking.NumberOfKids),
				new SqlParameter("@Phone", booking.Phone),
				new SqlParameter("@School", booking.School),
				new SqlParameter("@Town", booking.Town),
				new SqlParameter("@ZipCode", (object) booking.ZipCode ?? DBNull.Value)
			};

			ExecuteNonQuery(sql, parameters);
		}

		public void UpdateState(int id, bool isActive)
		{
			var sql = "UPDATE Booking SET IsActive = @IsActive WHERE Id = @Id";
			var parameters = new[]
			{
				new SqlParameter("@Id", id),
				new SqlParameter("@IsActive", isActive)
			};

			ExecuteNonQuery(sql, parameters);
		}

		protected override Booking MapFrom(SqlDataReader reader)
		{
			var booking = new Booking();

			booking.Id = (int) reader[nameof(Booking.Id)];
			booking.EventId = (int) reader[nameof(Booking.EventId)];
			booking.IsActive = (bool) reader[nameof(Booking.IsActive)];
			booking.Address = reader[nameof(Booking.Address)] as string;
			booking.Date = (DateTime) reader[nameof(Booking.Date)];
			booking.Email = (string) reader[nameof(Booking.Email)];
			booking.FirstName = (string) reader[nameof(Booking.FirstName)];
			booking.Grade = (string) reader[nameof(Booking.Grade)];
			booking.LastName = (string) reader[nameof(Booking.LastName)];
			booking.Notes = reader[nameof(Booking.Notes)] as string;
			booking.NumberOfKids = (int) reader[nameof(Booking.NumberOfKids)];
			booking.Phone = (string) reader[nameof(Booking.Phone)];
			booking.School = (string) reader[nameof(Booking.School)];
			booking.Town = (string) reader[nameof(Booking.Town)];
			booking.ZipCode = reader[nameof(Booking.ZipCode)] as int?;

			return booking;
		}
	}
}
