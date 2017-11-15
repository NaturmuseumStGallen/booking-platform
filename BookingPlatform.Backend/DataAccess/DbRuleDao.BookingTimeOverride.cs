/*
 * Copyright (C) 2017 Naturmuseum St. Gallen
 *  > https://github.com/NaturmuseumStGallen
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

using BookingPlatform.Backend.Entities;
using BookingPlatform.Backend.Entities.RuleConfigurations;
using System;
using System.Data.SqlClient;
using System.Transactions;

namespace BookingPlatform.Backend.DataAccess
{
    internal partial class DbRuleDao
    {
        public bool HasBookingTimeOverride(int eventId)
        {
            var sql = "SELECT COUNT(*) FROM BookingTimeOverrideRule WHERE EventId = @EventId";
            var parameter = new SqlParameter("@EventId", eventId);

            return Convert.ToInt32(ExecuteScalar(sql, parameter)) != 0;
        }

        private void DeleteBookingTimeOverride(BookingTimeOverrideRuleConfiguration config)
        {
            DeleteOverrideBookingTimes(config);

            var sql = "DELETE FROM BookingTimeOverrideRule WHERE RuleId = @Id";
            var parameter = new SqlParameter("@Id", config.RuleId);

            ExecuteNonQuery(sql, parameter);
        }

        private void LoadBookingTimeOverrideData(BookingTimeOverrideRuleConfiguration config)
        {
            var sql = "SELECT * FROM OverrideBookingTime WHERE BookingTimeOverrideRuleId = @Id";
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
                        config.OverrideBookingTimes.Add((TimeSpan)reader["Value"]);
                    }
                }

                transaction.Complete();
            }
        }

        private RuleConfiguration MapBookingTimeOverride(SqlDataReader reader)
        {
            var config = new BookingTimeOverrideRuleConfiguration();

            config.Id = (int)reader["bt_Id"];
            config.EventId = (int)reader["bt_EventId"];

            return config;
        }

        private void SaveBookingTimeOverride(BookingTimeOverrideRuleConfiguration config)
        {
            var sql = @"
			INSERT INTO
				BookingTimeOverrideRule(RuleId, EventId)
			VALUES
				(@RuleId, @EventId);
            SELECT SCOPE_IDENTITY()";

            var parameters = new[]
            {
                new SqlParameter("@RuleId", config.RuleId),
                new SqlParameter("@EventId", config.EventId),
            };

            config.Id = Convert.ToInt32(ExecuteScalar(sql, parameters));

            SaveBookingTimeOverrideData(config);
        }

        private void SaveBookingTimeOverrideData(BookingTimeOverrideRuleConfiguration config)
        {
            foreach(var bookingTime in config.OverrideBookingTimes)
            {
                var sql = @"
				INSERT INTO
					OverrideBookingTime([Value], BookingTimeOverrideRuleId)
				VALUES
					(@Value, @BookingTimeOverrideRuleId)";

                var parameters = new[]
                {
                    new SqlParameter("@Value", bookingTime),
                    new SqlParameter("@BookingTimeOverrideRuleId", config.Id)
                };

                ExecuteNonQuery(sql, parameters);
            }
        }

        private void UpdateBookingTimeOverride(BookingTimeOverrideRuleConfiguration config)
        {
            var sql = @"
			UPDATE
				BookingTimeOverrideRule
			SET
				EventId = @EventId
			WHERE
				Id = @Id";
            var parameters = new[]
            {
                new SqlParameter("@Id", config.Id),
                new SqlParameter("@EventId", config.EventId)
            };

            ExecuteNonQuery(sql, parameters);

            DeleteOverrideBookingTimes(config);
            SaveBookingTimeOverrideData(config);
        }

        private void DeleteOverrideBookingTimes(BookingTimeOverrideRuleConfiguration config)
        {
            var sql = "DELETE FROM OverrideBookingTime WHERE BookingTimeOverrideRuleId = @BookingTimeOverrideRuleId";
            var parameters = new[] { new SqlParameter("@BookingTimeOverrideRuleId", config.Id) };

            ExecuteNonQuery(sql, parameters);
        }
    }
}
