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
using BookingPlatform.Backend.Constants;
using BookingPlatform.Backend.Entities;
using BookingPlatform.Backend.Rules;

namespace BookingPlatform.Backend.DataAccess
{
	internal class DbRuleDao : IRuleProvider
	{
		public void Delete(int id)
		{
			
		}

		public IList<IRule> GetRules(DateTime from, DateTime to)
		{
			var rules = new List<IRule>();

			// TODO
			rules.Add(new WeeklyRule(DayOfWeek.Saturday, AvailabilityStatus.NotBookable));
			rules.Add(new WeeklyRule(DayOfWeek.Sunday, AvailabilityStatus.NotBookable));
			rules.Add(new DateRangeRule(new DateTime(2017, 4, 18, 10, 0, 0), new DateTime(2017, 4, 18, 12, 0, 0), AvailabilityStatus.Booked));

			return rules;
		}

		public IList<RuleData> GetRuleData()
		{
			var data = new List<RuleData>();

			data.Add(new RuleData { Id = 4, Name = "Some rule here", Type = RuleType.DateRange });
			data.Add(new RuleData { Id = 4, Name = "Some rule here", Type = RuleType.DateRange });
			data.Add(new RuleData { Id = 5, Name = "Some other rule here", Type = RuleType.EventGroup });
			data.Add(new RuleData { Id = 4, Name = "Some rule here", Type = RuleType.DateRange });

			return data;
		}

		public RuleData GetRuleData(int id)
		{
			return new RuleData { Id = 4, Name = "Some rule here", Type = RuleType.DateRange };
		}

		public void SaveNew(RuleData ruleData)
		{
			
		}

		public void Update(RuleData ruleData)
		{
			
		}
	}
}
