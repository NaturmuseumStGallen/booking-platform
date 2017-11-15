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
using System.Transactions;

namespace BookingPlatform.Backend.DataAccess
{
	internal partial class DbRuleDao
	{
		private void DeleteWeekly(WeeklyRuleConfiguration config)
		{
            DeleteEvents2Weekly(config);

            var sql = "DELETE FROM WeeklyRule WHERE RuleId = @Id";
			var parameter = new SqlParameter("@Id", config.RuleId);

			ExecuteNonQuery(sql, parameter);
		}

        private void DeleteEvents2Weekly(WeeklyRuleConfiguration config)
        {
            var sql = "DELETE FROM Event2WeeklyRule WHERE WeeklyRuleId = @WeeklyRuleId";
            var parameter = new SqlParameter("@WeeklyRuleId", config.Id);

            ExecuteNonQuery(sql, parameter);
        }

		private RuleConfiguration MapWeekly(SqlDataReader reader)
		{
			var config = new WeeklyRuleConfiguration();

			config.Id = (int)reader["wr_Id"];
			config.AvailabilityStatus = (AvailabilityStatus)reader["wr_AvailabilityStatusId"];
			config.DayOfWeek = (DayOfWeek)reader["wr_DayOfWeek"];
			config.StartDate = reader["wr_StartDate"] as DateTime?;
			config.Time = reader["wr_Time"] as TimeSpan?;
            config.EndTime = reader["wr_EndTime"] as TimeSpan?;

            return config;
		}

        private void LoadEvents2Weekly(WeeklyRuleConfiguration config)
        {
            var sql = "SELECT * FROM Event2WeeklyRule WHERE WeeklyRuleId = @Id";
            var parameter = new SqlParameter("@Id", config.Id);

            using (var transaction = new TransactionScope())
            using (var connection = NewSqlConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add(parameter);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        config.EventIds.Add((int)reader["EventId"]);
                    }
                }

                transaction.Complete();
            }
        }

        private void SaveWeekly(WeeklyRuleConfiguration config)
		{
			var sql = @"
			INSERT INTO
				WeeklyRule(RuleId, AvailabilityStatusId, [DayOfWeek], StartDate, [Time], EndTime)
			VALUES
				(@RuleId, @AvailabilityStatusId, @DayOfWeek, @StartDate, @Time, @EndTime);
            SELECT SCOPE_IDENTITY()";

			var parameters = new[]
			{
				new SqlParameter("@RuleId", config.RuleId),
				new SqlParameter("@AvailabilityStatusId", config.AvailabilityStatus),
				new SqlParameter("@DayOfWeek", config.DayOfWeek),
				new SqlParameter("@StartDate", (object) config.StartDate ?? DBNull.Value),
				new SqlParameter("@Time", (object) config.Time ?? DBNull.Value),
                new SqlParameter("@EndTime", (object) config.EndTime ?? DBNull.Value)
            };

            config.Id = Convert.ToInt32(ExecuteScalar(sql, parameters));

            SaveEvents2Weekly(config);
        }

        private void SaveEvents2Weekly(WeeklyRuleConfiguration config)
        {
            if (config.EventIds == null)
                return;

            foreach (var id in config.EventIds)
            {
                var sql = @"
				INSERT INTO
					Event2WeeklyRule(EventId, WeeklyRuleId)
				VALUES
					(@EventId, @WeeklyRuleId)";

                var parameters = new[]
                {
                    new SqlParameter("@EventId", id),
                    new SqlParameter("@WeeklyRuleId", config.Id)
                };

                ExecuteNonQuery(sql, parameters);
            }
        }

        private void UpdateWeekly(WeeklyRuleConfiguration config)
		{
			var sql = @"
			UPDATE
				WeeklyRule
			SET
				AvailabilityStatusId = @AvailabilityStatusId,
				[DayOfWeek] = @DayOfWeek,
				StartDate = @StartDate,
				[Time] = @Time,
                EndTime = @EndTime
			WHERE
				Id = @Id";
			var parameters = new[]
			{
				new SqlParameter("@Id", config.Id),
				new SqlParameter("@AvailabilityStatusId", config.AvailabilityStatus),
				new SqlParameter("@DayOfWeek", config.DayOfWeek),
				new SqlParameter("@StartDate", (object) config.StartDate ?? DBNull.Value),
				new SqlParameter("@Time", (object) config.Time ?? DBNull.Value),
                new SqlParameter("@EndTime", (object) config.EndTime ?? DBNull.Value)
            };

			ExecuteNonQuery(sql, parameters);

            DeleteEvents2Weekly(config);
            SaveEvents2Weekly(config);
        }
	}
}
