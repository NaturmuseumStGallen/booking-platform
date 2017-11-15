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
using System.Data.SqlClient;

namespace BookingPlatform.Backend.DataAccess
{
    internal partial class DbRuleDao
    {
        private void DeleteMultipleBooking(MultipleBookingRuleConfiguration config)
        {
            var sql = "DELETE FROM MultipleBookingRule WHERE RuleId = @Id";
            var parameter = new SqlParameter("@Id", config.RuleId);

            ExecuteNonQuery(sql, parameter);
        }

        private RuleConfiguration MapMultipleBooking(SqlDataReader reader)
        {
            var config = new MultipleBookingRuleConfiguration();

            config.Id = (int)reader["mb_Id"];
            config.EventId = (int)reader["mb_EventId"];
            config.NumberOfParallelBookings = (int)reader["mb_NumberOfParallelBookings"];

            return config;
        }

        private void SaveMultipleBooking(MultipleBookingRuleConfiguration config)
        {
            var sql = @"
			INSERT INTO
				MultipleBookingRule(RuleId, EventId, NumberOfParallelBookings)
			VALUES
				(@RuleId, @EventId, @NumberOfParallelBookings)";
            var parameters = new[]
            {
                new SqlParameter("@RuleId", config.RuleId),
                new SqlParameter("@EventId", config.EventId),
                new SqlParameter("@NumberOfParallelBookings", config.NumberOfParallelBookings)
            };

            ExecuteNonQuery(sql, parameters);
        }

        private void UpdateMultipleBooking(MultipleBookingRuleConfiguration config)
        {
            var sql = @"
			UPDATE
				MultipleBookingRule
			SET
				RuleId = @RuleId,
				EventId = @EventId,
				NumberOfParallelBookings = @NumberOfParallelBookings
			WHERE
				Id = @Id";
            var parameters = new[]
            {
                new SqlParameter("@Id", config.Id),
                new SqlParameter("@RuleId", config.RuleId),
                new SqlParameter("@EventId", config.EventId),
                new SqlParameter("@NumberOfParallelBookings", config.NumberOfParallelBookings)
            };

            ExecuteNonQuery(sql, parameters);
        }
    }
}
