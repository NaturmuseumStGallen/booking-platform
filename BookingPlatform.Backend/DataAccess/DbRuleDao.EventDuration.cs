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
		private void DeleteEventDuration(EventDurationRuleConfiguration config)
		{
			var sql = "DELETE FROM EventDurationRule WHERE RuleId = @Id";
			var parameter = new SqlParameter("@Id", config.RuleId);

			ExecuteNonQuery(sql, parameter);
		}

		private RuleConfiguration MapEventDuration(SqlDataReader reader)
		{
			var config = new EventDurationRuleConfiguration();

			config.Id = (int)reader["ed_Id"];
			config.EventId = (int)reader["ed_EventId"];
			config.EndDate = (DateTime)reader["ed_EndDate"];
			config.StartDate = (DateTime)reader["ed_StartDate"];

			return config;
		}

		private void SaveEventDuration(EventDurationRuleConfiguration config)
		{
			var sql = @"
			INSERT INTO
				EventDurationRule(RuleId, EventId, StartDate, EndDate)
			VALUES
				(@RuleId, @EventId, @StartDate, @EndDate)";
			var parameters = new[]
			{
				new SqlParameter("@RuleId", config.RuleId),
				new SqlParameter("@EventId", config.EventId),
				new SqlParameter("@StartDate", config.StartDate),
				new SqlParameter("@EndDate", config.EndDate)
			};

			ExecuteNonQuery(sql, parameters);
		}

		private void UpdateEventDuration(EventDurationRuleConfiguration config)
		{
			var sql = @"
			UPDATE
				EventDurationRule
			SET
				EventId = @EventId,
				StartDate = @StartDate,
				EndDate = @EndDate
			WHERE
				Id = @Id";
			var parameters = new[]
			{
				new SqlParameter("@Id", config.Id),
				new SqlParameter("@EventId", config.EventId),
				new SqlParameter("@StartDate", config.StartDate),
				new SqlParameter("@EndDate", config.EndDate),
			};

			ExecuteNonQuery(sql, parameters);
		}
	}
}
