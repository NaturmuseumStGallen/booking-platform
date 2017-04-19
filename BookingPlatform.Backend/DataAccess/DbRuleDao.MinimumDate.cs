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
		private void DeleteMinimumDate(MinimumDateRuleConfiguration config)
		{
			var sql = "DELETE FROM MinimumDateRule WHERE RuleId = @Id";
			var parameter = new SqlParameter("@Id", config.RuleId);

			ExecuteNonQuery(sql, parameter);
		}

		private RuleConfiguration MapMinimumDate(SqlDataReader reader)
		{
			var config = new MinimumDateRuleConfiguration();

			config.Id = (int)reader["md_Id"];
			config.Date = reader["md_Date"] as DateTime?;
			config.Days = reader["md_Days"] as int?;

			return config;
		}

		private void SaveMinimumDate(MinimumDateRuleConfiguration config)
		{
			var sql = @"
			INSERT INTO
				MinimumDateRule(RuleId, Date, Days)
			VALUES
				(@RuleId, @Date, @Days)";
			var parameters = new[]
			{
				new SqlParameter("@RuleId", config.RuleId),
				new SqlParameter("@Date", (object) config.Date ?? DBNull.Value),
				new SqlParameter("@Days", (object) config.Days ?? DBNull.Value)
			};

			ExecuteNonQuery(sql, parameters);
		}

		private void UpdateMinimumDate(MinimumDateRuleConfiguration config)
		{
			var sql = @"
			UPDATE
				MinimumDateRule
			SET
				[Date] = @Date,
				[Days] = @Days
			WHERE
				Id = @Id";
			var parameters = new[]
			{
				new SqlParameter("@Id", config.Id),
				new SqlParameter("@Date", (object) config.Date ?? DBNull.Value),
				new SqlParameter("@Days", (object) config.Days ?? DBNull.Value)
			};

			ExecuteNonQuery(sql, parameters);
		}
	}
}
