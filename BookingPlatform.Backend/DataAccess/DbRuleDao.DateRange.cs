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
using System.Data.SqlClient;
using BookingPlatform.Backend.Entities;
using BookingPlatform.Backend.Entities.RuleConfigurations;

namespace BookingPlatform.Backend.DataAccess
{
	internal partial class DbRuleDao
	{
		private void DeleteDateRange(DateRangeRuleConfiguration config)
		{
			var sql = "DELETE FROM DateRangeRule WHERE RuleId = @Id";
			var parameter = new SqlParameter("@Id", config.RuleId);

			ExecuteNonQuery(sql, parameter);
		}

		private RuleConfiguration MapDateRange(SqlDataReader reader)
		{
			var config = new DateRangeRuleConfiguration();

			config.Id = (int)reader["dr_Id"];
			config.AvailabilityStatus = (AvailabilityStatus)reader["dr_AvailabilityStatusId"];
			config.EndDate = reader["dr_EndDate"] as DateTime?;
			config.EndTime = reader["dr_EndTime"] as TimeSpan?;
			config.StartDate = (DateTime)reader["dr_StartDate"];
			config.StartTime = reader["dr_StartTime"] as TimeSpan?;

			return config;
		}

		private void SaveDateRange(DateRangeRuleConfiguration config)
		{
			var sql = @"
			INSERT INTO
				DateRangeRule(RuleId, AvailabilityStatusId, EndDate, EndTime, StartDate, StartTime)
			VALUES
				(@RuleId, @AvailabilityStatusId, @EndDate, @EndTime, @StartDate, @StartTime)";
			var parameters = new[]
			{
				new SqlParameter("@RuleId", config.RuleId),
				new SqlParameter("@AvailabilityStatusId", config.AvailabilityStatus),
				new SqlParameter("@EndDate", (object) config.EndDate ?? DBNull.Value),
				new SqlParameter("@EndTime", (object) config.EndTime ?? DBNull.Value),
				new SqlParameter("@StartDate", config.StartDate),
				new SqlParameter("@StartTime", (object) config.StartTime ?? DBNull.Value)
			};

			ExecuteNonQuery(sql, parameters);
		}


		private void UpdateDateRange(DateRangeRuleConfiguration config)
		{
			var sql = @"
			UPDATE
				DateRangeRule
			SET
				AvailabilityStatusId = @AvailabilityStatusId,
				EndDate = @EndDate,
				EndTime = @EndTime,
				StartDate = @StartDate,
				StartTime = @StartTime
			WHERE
				Id = @Id";
			var parameters = new[]
			{
				new SqlParameter("@Id", config.Id),
				new SqlParameter("@AvailabilityStatusId", config.AvailabilityStatus),
				new SqlParameter("@EndDate", (object) config.EndDate ?? DBNull.Value),
				new SqlParameter("@EndTime", (object) config.EndTime ?? DBNull.Value),
				new SqlParameter("@StartDate", config.StartDate),
				new SqlParameter("@StartTime", (object) config.StartTime ?? DBNull.Value)
			};

			ExecuteNonQuery(sql, parameters);
		}
	}
}
