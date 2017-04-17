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
using BookingPlatform.Backend.Scheduling;

namespace BookingPlatform.Backend.DataAccess
{
	internal class DbRuleDao : DbBaseDao<RuleConfiguration>, IRuleProvider
	{
		public void Delete(int id)
		{
			
		}

		public IList<IRule> GetRules()
		{
			var ruleData = GetRuleConfigurations();
			var rules = new List<IRule>();

			foreach (var data in ruleData)
			{
				rules.Add(ToRule(data));
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

		public RuleConfiguration GetRuleData(int id)
		{
			return new RuleConfiguration { RuleId = 4, Name = "Some rule here", Type = RuleType.DateRange };
		}

		public void SaveNew(RuleConfiguration config)
		{
			
		}

		public void Update(RuleConfiguration config)
		{
			
		}

		protected override RuleConfiguration MapFrom(SqlDataReader reader)
		{
			RuleConfiguration config = null;
			var type = (RuleType) reader["r.RuleTypeId"];

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

			config.RuleId = (int) reader["r.Id"];
			config.Type = type;
			config.Name = (string) reader["r.Name"];

			return config;
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

		private IRule ToRule(RuleConfiguration config)
		{
			IRule rule = null;

			switch (config.Type)
			{
				case RuleType.DateRange:
					rule = ToDateRangeRule(config as DateRangeRuleConfiguration);
					break;
				case RuleType.EventGroup:
					rule = ToEventGroupRule(config as EventGroupRuleConfiguration);
					break;
				case RuleType.MinimumDate:
					rule = ToMinimumDateRule(config as MinimumDateRuleConfiguration);
					break;
				case RuleType.Weekly:
					rule = ToWeeklyRule(config as WeeklyRuleConfiguration);
					break;
				default:
					throw new InvalidOperationException(String.Format("Rule of type '{0}' not yet configured!", config.Type));
			}

			return rule;
		}

		private IRule ToDateRangeRule(DateRangeRuleConfiguration config)
		{
			var from = DateTimeUtility.NewFor(config.StartDate, config.StartTime);
			var to = new DateTime();

			if (config.EndDate.HasValue)
			{
				to = DateTimeUtility.NewFor(config.EndDate.Value, config.EndTime);
			}
			else if (config.StartTime.HasValue)
			{
				to = from;
			}
			else
			{
				to = from.AddDays(1);
			}

			return new DateRangeRule(from, to, config.AvailabilityStatus);
		}

		private IRule ToEventGroupRule(EventGroupRuleConfiguration config)
		{
			var group = new EventGroup();

			group.Bookings = new DbBookingDao().GetByEvents(config.EventIds);
			group.Events = new DbEventDao().GetBy(config.EventIds);

			return new EventGroupRule(group, AvailabilityStatus.Booked);
		}

		private IRule ToMinimumDateRule(MinimumDateRuleConfiguration config)
		{
			throw new NotImplementedException();
		}

		private IRule ToWeeklyRule(WeeklyRuleConfiguration config)
		{
			throw new NotImplementedException();
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
	}
}
