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

using System.Collections.Generic;
using BookingPlatform.Backend.DataAccess;
using BookingPlatform.Backend.Rules;

namespace BookingPlatform.Backend.Entities.RuleConfigurations
{
	public class EventGroupRuleConfiguration : RuleConfiguration
	{
		public EventGroupRuleConfiguration()
		{
			EventIds = new List<int>();
		}

		public int Id { get; set; }
		public IList<int> EventIds { get; set; }

		public override IRule ToRule()
		{
			var group = new EventGroup();

			group.Bookings = new DbBookingDao().GetByEvents(EventIds);
			group.Events = new DbEventDao().GetBy(EventIds);

			return new EventGroupRule(group, AvailabilityStatus.Booked);
		}
	}
}
