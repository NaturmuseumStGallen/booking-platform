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
using System.Transactions;
using BookingPlatform.Backend.Constants;
using BookingPlatform.Backend.Entities;
using BookingPlatform.Backend.Entities.RuleConfigurations;
using BookingPlatform.Backend.Rules;

namespace BookingPlatform.Backend.DataAccess
{
	internal class DbRuleDao : DbBaseDao<RuleConfiguration>, IRuleProvider
	{
		public void Delete(int id)
		{
			var config = GetConfigurationBy(id);
			var sqls = new List<string>();
			var parameters = new List<SqlParameter>();

			switch (config.Type)
			{
				case RuleType.DateRange:
					sqls.Add("DELETE FROM DateRangeRule WHERE RuleId = @Id");
					break;
				case RuleType.EventGroup:
					sqls.Add("DELETE FROM Event2EventGroupRule WHERE EventGroupRuleId = @EventGroupRuleId");
					sqls.Add("DELETE FROM EventGroupRule WHERE RuleId = @Id");
					parameters.Add(new SqlParameter("@EventGroupRuleId", (config as EventGroupRuleConfiguration).Id));
					break;
				case RuleType.MinimumDate:
					sqls.Add("DELETE FROM MinimumDateRule WHERE RuleId = @Id");
					break;
				case RuleType.Weekly:
					sqls.Add("DELETE FROM WeeklyRule WHERE RuleId = @Id");
					break;
				default:
					throw new InvalidOperationException(String.Format("Rule of type '{0}' not yet configured!", config.Type));
			}

			sqls.Add("DELETE FROM [Rule] WHERE Id = @Id");
			parameters.Add(new SqlParameter("@Id", id));

			foreach (var sql in sqls)
			{
				ExecuteNonQuery(sql, parameters.ToArray());
			}
		}

		public IList<IRule> GetRules()
		{
			var configs = GetRuleConfigurations();
			var rules = new List<IRule>();

			foreach (var config in configs)
			{
				rules.Add(config.ToRule());
			}

			return rules;
		}

		public IList<RuleConfiguration> GetRuleConfigurations()
		{
			var sql = @"
			SELECT *
			FROM [Rule] AS r
			FULL OUTER JOIN DateRangeRule AS dr ON r.Id = dr.RuleId
			FULL OUTER JOIN EventGroupRule AS eg ON r.Id = eg.RuleId
			FULL OUTER JOIN MinimumDateRule AS md ON r.Id = md.RuleId
			FULL OUTER JOIN WeeklyRule AS wr ON r.Id = wr.RuleId";
			var configs = ExecuteMultiQuery(sql);

			foreach (var config in configs)
			{
				if (config is EventGroupRuleConfiguration)
				{
					LoadEventGroupData(config as EventGroupRuleConfiguration);
				}
			}

			return configs;
		}

		public RuleConfiguration GetConfigurationBy(int id)
		{
			var sql = @"
			SELECT *
			FROM [Rule] AS r
			FULL OUTER JOIN DateRangeRule AS dr ON r.Id = dr.RuleId
			FULL OUTER JOIN EventGroupRule AS eg ON r.Id = eg.RuleId
			FULL OUTER JOIN MinimumDateRule AS md ON r.Id = md.RuleId
			FULL OUTER JOIN WeeklyRule AS wr ON r.Id = wr.RuleId
			WHERE r.Id = @Id";
			var parameter = new SqlParameter("@Id", id);
			var config = ExecuteSingleQuery(sql, parameter);

			if (config is EventGroupRuleConfiguration)
			{
				LoadEventGroupData(config as EventGroupRuleConfiguration);
			}

			return config;
		}

		public void SaveNew(RuleConfiguration config)
		{
			var sql = @"
			INSERT INTO
				[Rule](RuleTypeId, Name)
			VALUES
				(@RuleTypeId, @Name);
			SELECT SCOPE_IDENTITY()";
			var parameters = new[]
			{
				new SqlParameter("@RuleTypeId", config.Type),
				new SqlParameter("@Name", config.Name)
			};

			config.RuleId = Convert.ToInt32(ExecuteScalar(sql, parameters));

			switch (config.Type)
			{
				case RuleType.DateRange:
					SaveDateRange(config as DateRangeRuleConfiguration);
					break;
				case RuleType.EventGroup:
					SaveEventGroup(config as EventGroupRuleConfiguration);
					break;
				case RuleType.MinimumDate:
					SaveMinimumDate(config as MinimumDateRuleConfiguration);
					break;
				case RuleType.Weekly:
					SaveWeekly(config as WeeklyRuleConfiguration);
					break;
				default:
					throw new InvalidOperationException(String.Format("Rule of type '{0}' not yet configured!", config.Type));
			}
		}

		public void Update(RuleConfiguration config)
		{
			var sql = @"
			UPDATE
				[Rule]
			SET
				Name = @Name
			WHERE
				Id = @Id";
			var parameters = new[]
			{
				new SqlParameter("@Id", config.RuleId),
				new SqlParameter("@Name", config.Name)
			};

			ExecuteNonQuery(sql, parameters);

			switch (config.Type)
			{
				case RuleType.DateRange:
					UpdateDateRange(config as DateRangeRuleConfiguration);
					break;
				case RuleType.EventGroup:
					UpdateEventGroup(config as EventGroupRuleConfiguration);
					break;
				case RuleType.MinimumDate:
					UpdateMinimumDate(config as MinimumDateRuleConfiguration);
					break;
				case RuleType.Weekly:
					UpdateWeekly(config as WeeklyRuleConfiguration);
					break;
				default:
					throw new InvalidOperationException(String.Format("Rule of type '{0}' not yet configured!", config.Type));
			}
		}

		protected override RuleConfiguration MapFrom(SqlDataReader reader)
		{
			RuleConfiguration config = null;
			var type = (RuleType)reader["r.RuleTypeId"];

			switch (type)
			{
				case RuleType.DateRange:
					config = MapDateRange(reader);
					break;
				case RuleType.EventGroup:
					config = MapEventGroup(reader);
					break;
				case RuleType.MinimumDate:
					config = MapMinimumDate(reader);
					break;
				case RuleType.Weekly:
					config = MapWeekly(reader);
					break;
				default:
					throw new InvalidOperationException(String.Format("Rule of type '{0}' not yet configured!", type));
			}

			config.RuleId = (int)reader["r.Id"];
			config.Type = type;
			config.Name = (string)reader["r.Name"];

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
				new SqlParameter("@EndDate", config.EndDate),
				new SqlParameter("@EndTime", config.EndTime),
				new SqlParameter("@StartDate", config.StartDate),
				new SqlParameter("@StartTime", config.StartTime)
			};

			ExecuteNonQuery(sql, parameters);
		}

		private void SaveEventGroup(EventGroupRuleConfiguration config)
		{
			var sql = @"
			INSERT INTO
				EventGroupRule(RuleId)
			VALUES
				(@RuleId)";
			var parameters = new[] { new SqlParameter("@RuleId", config.RuleId) };

			config.Id = Convert.ToInt32(ExecuteScalar(sql, parameters));

			SaveEventGroupData(config);
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
				new SqlParameter("@Date", config.Date),
				new SqlParameter("@Days", config.Days)
			};

			ExecuteNonQuery(sql, parameters);
		}

		private void SaveWeekly(WeeklyRuleConfiguration config)
		{
			var sql = @"
			INSERT INTO
				WeeklyRule(RuleId, AvailabilityStatusId, [DayOfWeek], StartDate, [Time])
			VALUES
				(@RuleId, @AvailabilityStatusId, @DayOfWeek, @StartDate, @Time)";
			var parameters = new[]
			{
				new SqlParameter("@RuleId", config.RuleId),
				new SqlParameter("@AvailabilityStatusId", config.AvailabilityStatus),
				new SqlParameter("@DayOfWeek", config.DayOfWeek),
				new SqlParameter("@StartDate", config.StartDate),
				new SqlParameter("@Time", config.Time)
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
				new SqlParameter("@EndDate", config.EndDate),
				new SqlParameter("@EndTime", config.EndTime),
				new SqlParameter("@StartDate", config.StartDate),
				new SqlParameter("@StartTime", config.StartTime)
			};

			ExecuteNonQuery(sql, parameters);
		}

		private void UpdateEventGroup(EventGroupRuleConfiguration config)
		{
			var sql = "DELETE FROM Event2EventGroupRule WHERE EventGroupRuleId = @EventGroupRuleId";
			var parameters = new[] { new SqlParameter("@EventGroupRuleId", config.Id) };

			ExecuteNonQuery(sql, parameters);
			SaveEventGroupData(config);
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
				new SqlParameter("@Date", config.Date),
				new SqlParameter("@Days", config.Days)
			};

			ExecuteNonQuery(sql, parameters);
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
				[Time] = @Time
			WHERE
				Id = @Id";
			var parameters = new[]
			{
				new SqlParameter("@Id", config.Id),
				new SqlParameter("@AvailabilityStatusId", config.AvailabilityStatus),
				new SqlParameter("@DayOfWeek", config.DayOfWeek),
				new SqlParameter("@StartDate", config.StartDate),
				new SqlParameter("@Time", config.Time)
			};

			ExecuteNonQuery(sql, parameters);
		}

		private RuleConfiguration MapDateRange(SqlDataReader reader)
		{
			var config = new DateRangeRuleConfiguration();

			config.Id = (int) reader["dr.Id"];
			config.AvailabilityStatus = (AvailabilityStatus) reader["dr.AvailabilityStatus"];
			config.EndDate = (DateTime?) reader["dr.EndDate"];
			config.EndTime = (TimeSpan?) reader["dr.EndTime"];
			config.StartDate = (DateTime) reader["dr.StartDate"];
			config.StartTime = (TimeSpan?) reader["dr.StartTime"];

			return config;
		}

		private RuleConfiguration MapEventGroup(SqlDataReader reader)
		{
			var config = new EventGroupRuleConfiguration();

			config.Id = (int) reader["eg.Id"];

			return config;
		}

		private RuleConfiguration MapMinimumDate(SqlDataReader reader)
		{
			var config = new MinimumDateRuleConfiguration();

			config.Id = (int) reader["md.Id"];
			config.Date = (DateTime?) reader["md.Date"];
			config.Days = (int?) reader["md.Days"];

			return config;
		}

		private RuleConfiguration MapWeekly(SqlDataReader reader)
		{
			var config = new WeeklyRuleConfiguration();

			config.Id = (int) reader["wr.Id"];
			config.AvailabilityStatus = (AvailabilityStatus) reader["wr.AvailabilityStatus"];
			config.DayOfWeek = (DayOfWeek) reader["wr.DayOfWeek"];
			config.StartDate = (DateTime?) reader["wr.StartDate"];
			config.Time = (TimeSpan?) reader["wr.Time"];

			return config;
		}

		private void LoadEventGroupData(EventGroupRuleConfiguration config)
		{
			var sql = "SELECT * FROM Event2EventGroupRule WHERE EventGroupRuleId = @Id";
			var parameter = new SqlParameter("@Id", config.Id);

			using (var transaction = new TransactionScope())
			using (var connection = NewSqlConnection())
			using (var command = new SqlCommand(sql, connection))
			{
				command.Parameters.Add(parameter);

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						config.EventIds.Add((int) reader["EventId"]);
					}
				}

				transaction.Complete();
			}
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
	}
}
