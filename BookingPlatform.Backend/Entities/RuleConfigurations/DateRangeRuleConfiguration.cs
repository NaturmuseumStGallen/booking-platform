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
using BookingPlatform.Backend.Rules;
using BookingPlatform.Backend.Scheduling;

namespace BookingPlatform.Backend.Entities.RuleConfigurations
{
	public class DateRangeRuleConfiguration : RuleConfiguration
	{
		public int Id { get; set; }
		public AvailabilityStatus AvailabilityStatus { get; set; }
		public DateTime? EndDate { get; set; }
		public TimeSpan? EndTime { get; set; }
		public DateTime StartDate { get; set; }
		public TimeSpan? StartTime { get; set; }

		internal override IRule ToRule()
		{
			var from = DateTimeUtility.NewFor(StartDate, StartTime);
			var to = new DateTime();

			if (EndDate.HasValue)
			{
				to = DateTimeUtility.NewFor(EndDate.Value, EndTime);
			}
			else if (StartTime.HasValue)
			{
				to = from;
			}
			else
			{
				to = from.AddDays(1);
			}

			return new DateRangeRule(from, to, AvailabilityStatus);
		}
	}
}
