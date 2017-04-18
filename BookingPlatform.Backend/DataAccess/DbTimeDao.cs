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
	internal class DbTimeDao : DbBaseDao<TimeData>, ITimeProvider
	{
		public void Delete(int id)
		{
			var sql = "DELETE FROM [Time] WHERE Id = @Id";
			var parameter = new SqlParameter("@Id", id);

			ExecuteNonQuery(sql, parameter);
		}

		public IList<TimeSpan> GetTimes()
		{
			return GetTimeData().Select(t => t.Value).ToList();
		}

		public IList<TimeData> GetTimeData()
		{
			var sql = "SELECT * FROM [Time]";

			return ExecuteMultiQuery(sql);
		}

		public void SaveNew(TimeSpan time)
		{
			var sql = "INSERT INTO [Time]([Value]) VALUES (@Value)";
			var parameter = new SqlParameter("@Value", time);

			ExecuteNonQuery(sql, parameter);
		}

		protected override TimeData MapFrom(SqlDataReader reader)
		{
			var time = new TimeData();

			time.Id = (int) reader[nameof(TimeData.Id)];
			time.Value = (TimeSpan) reader[nameof(TimeData.Value)];

			return time;
		}
	}
}
