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
using System.Transactions;
using BookingPlatform.Backend.Entities;
using BookingPlatform.Backend.Entities.RuleConfigurations;

namespace BookingPlatform.Backend.DataAccess
{
	internal partial class DbRuleDao
	{
		public void RemoveFromEventGroups(int eventId)
		{
			var sql = "DELETE FROM Event2EventGroupRule WHERE EventId = @EventId";
			var parameter = new SqlParameter("@EventId", eventId);

			ExecuteNonQuery(sql, parameter);
		}

		private void DeleteEventGroup(EventGroupRuleConfiguration config)
		{
			var sql = "DELETE FROM Event2EventGroupRule WHERE EventGroupRuleId = @EventGroupRuleId";
			var parameter = new SqlParameter("@EventGroupRuleId", config.Id);

			ExecuteNonQuery(sql, parameter);

			sql = "DELETE FROM EventGroupRule WHERE RuleId = @Id";
			parameter = new SqlParameter("@Id", config.RuleId);

			ExecuteNonQuery(sql, parameter);
		}

		private void LoadEventGroupData(EventGroupRuleConfiguration config)
		{
			var sql = "SELECT * FROM Event2EventGroupRule WHERE EventGroupRuleId = @Id";
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

		private RuleConfiguration MapEventGroup(SqlDataReader reader)
		{
			var config = new EventGroupRuleConfiguration();

			config.Id = (int)reader["eg_Id"];

			return config;
		}

		private void SaveEventGroup(EventGroupRuleConfiguration config)
		{
			var sql = @"
			INSERT INTO
				EventGroupRule(RuleId)
			VALUES
				(@RuleId);
			SELECT SCOPE_IDENTITY()";
			var parameters = new[] { new SqlParameter("@RuleId", config.RuleId) };

			config.Id = Convert.ToInt32(ExecuteScalar(sql, parameters));

			SaveEventGroupData(config);
		}

		private void SaveEventGroupData(EventGroupRuleConfiguration config)
		{
			foreach (var id in config.EventIds)
			{
				var sql = @"
				INSERT INTO
					Event2EventGroupRule(EventId, EventGroupRuleId)
				VALUES
					(@EventId, @EventGroupRuleId)";

				var parameters = new[]
				{
					new SqlParameter("@EventId", id),
					new SqlParameter("@EventGroupRuleId", config.Id)
				};

				ExecuteNonQuery(sql, parameters);
			}
		}

		private void UpdateEventGroup(EventGroupRuleConfiguration config)
		{
			var sql = "DELETE FROM Event2EventGroupRule WHERE EventGroupRuleId = @EventGroupRuleId";
			var parameters = new[] { new SqlParameter("@EventGroupRuleId", config.Id) };

			ExecuteNonQuery(sql, parameters);
			SaveEventGroupData(config);
		}
	}
}
