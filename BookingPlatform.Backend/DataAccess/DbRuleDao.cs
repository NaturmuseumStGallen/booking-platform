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
using BookingPlatform.Backend.Constants;
using BookingPlatform.Backend.Entities;
using BookingPlatform.Backend.Entities.RuleConfigurations;
using BookingPlatform.Backend.Rules;

namespace BookingPlatform.Backend.DataAccess
{
	internal partial class DbRuleDao : DbBaseDao<RuleConfiguration>, IRuleProvider
	{
		private const string SELECT_STATEMENT = @"
			SELECT
				r.Id AS r_Id,
				r.RuleTypeId AS r_RuleTypeId,
				r.Name AS r_Name,
				dr.Id AS dr_Id,
				dr.AvailabilityStatusId AS dr_AvailabilityStatusId,
				dr.EndDate AS dr_EndDate,
				dr.EndTime AS dr_EndTime,
				dr.StartDate AS dr_StartDate,
				dr.StartTime AS dr_StartTime,
				eg.Id AS eg_Id,
				md.Id AS md_Id,
				md.Date AS md_Date,
				md.Days AS md_Days,
				wr.Id AS wr_Id,
				wr.AvailabilityStatusId AS wr_AvailabilityStatusId,
				wr.DayOfWeek AS wr_DayOfWeek,
				wr.StartDate AS wr_StartDate,
				wr.Time AS wr_Time
			FROM
				[Rule] AS r
			FULL OUTER JOIN
				DateRangeRule AS dr ON r.Id = dr.RuleId
			FULL OUTER JOIN
				EventGroupRule AS eg ON r.Id = eg.RuleId
			FULL OUTER JOIN
				MinimumDateRule AS md ON r.Id = md.RuleId
			FULL OUTER JOIN
				WeeklyRule AS wr ON r.Id = wr.RuleId";

		public void Delete(int id)
		{
			var config = GetConfigurationBy(id);
			var sql = "DELETE FROM [Rule] WHERE Id = @Id";
			var parameter = new SqlParameter("@Id", id);

			switch (config.Type)
			{
				case RuleType.DateRange:
					DeleteDateRange(config as DateRangeRuleConfiguration);
					break;
				case RuleType.EventGroup:
					DeleteEventGroup(config as EventGroupRuleConfiguration);
					break;
				case RuleType.MinimumDate:
					DeleteMinimumDate(config as MinimumDateRuleConfiguration);
					break;
				case RuleType.Weekly:
					DeleteWeekly(config as WeeklyRuleConfiguration);
					break;
				default:
					throw new InvalidOperationException(String.Format("Rule of type '{0}' not yet configured!", config.Type));
			}

			ExecuteNonQuery(sql, parameter);
		}

		public bool Exists(int id)
		{
			var sql = "SELECT COUNT(*) FROM [Rule] WHERE Id = @Id";
			var parameter = new SqlParameter("@Id", id);

			return Convert.ToInt32(ExecuteScalar(sql, parameter)) == 1;
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
			var configs = ExecuteMultiQuery(SELECT_STATEMENT);

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
			var sql = SELECT_STATEMENT + " WHERE r.Id = @Id";
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
			var type = (RuleType) reader["r_RuleTypeId"];

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

			config.RuleId = (int) reader["r_Id"];
			config.Type = type;
			config.Name = (string) reader["r_Name"];

			return config;
		}
	}
}
