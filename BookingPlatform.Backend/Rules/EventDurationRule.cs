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
using BookingPlatform.Backend.Constants;
using BookingPlatform.Backend.Entities;
using BookingPlatform.Backend.Scheduling;

namespace BookingPlatform.Backend.Rules
{
	public class EventDurationRule : IRule
	{
		private int eventId;
		private DateTime start;
		private DateTime end;

		public RuleType Type
		{
			get { return RuleType.EventDuration; }
		}

		/// <summary>
		/// Defines a new event duration rule for the specified date range, i.e. all dates lying
		/// outside the date range will not be bookable for the given event. This rule regards only
		/// the date aspect of the start and end values!
		/// </summary>
		public EventDurationRule(int eventId, DateTime start, DateTime end)
		{
			this.eventId = eventId;
			this.start = start;
			this.end = end;
		}

		public AvailabilityStatus GetStatus(DateTime date, Event @event)
		{
			if (eventId == @event.Id && (date.Date.IsSmallerThan(start.Date) || date.Date.IsBiggerThan(end.Date)))
			{
				return AvailabilityStatus.NotBookable;
			}

			return AvailabilityStatus.Undefined;
		}
	}
}
