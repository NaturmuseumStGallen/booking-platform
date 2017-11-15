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
using System.Collections.Generic;

namespace BookingPlatform.Backend.Rules
{
	public class WeeklyRule : IStandardRule
    {
		private DayOfWeek day;
		private AvailabilityStatus status;
		private TimeSpan? time;
        private TimeSpan? endTime;
        private DateTime? startDate;
        private IList<int> eventIds;

        public RuleType Type
		{
			get { return RuleType.Weekly; }
		}

		/// <summary>
		/// Defines a weekly recurring rule which returns the specified status for events happening
		/// on the defined day of week. If no time is defined, the rule will apply for the whole day.
		/// Optionally, a start date may be configured to define from when on the rule should be in effect.
		/// </summary>
		public WeeklyRule(DayOfWeek day, AvailabilityStatus status, TimeSpan? time = null, TimeSpan? endTime = null, DateTime? startDate = null, IList<int> eventIds = null)
		{
			this.day = day;
			this.status = status;
			this.time = time;
            this.endTime = endTime;
			this.startDate = startDate;
            this.eventIds = eventIds;
		}

		public AvailabilityStatus GetStatus(DateTime date, Event @event)
		{
            if (eventIds != null && eventIds.Count > 0 && !eventIds.Contains(@event.Id.Value))
                return AvailabilityStatus.Undefined;

			if (!startDate.HasValue || (startDate.HasValue && startDate.Value.IsSmallerThanOrEqualAs(date)))
			{
				if (date.DayOfWeek == day)
				{
					if (!time.HasValue && !endTime.HasValue)
					{
						return status;
					}

					if (!endTime.HasValue && time.Value.Hours == date.Hour && time.Value.Minutes == date.Minute)
					{
						return status;
					}

                    if (!time.HasValue && date.TimeOfDay.CompareTo(endTime.Value) < 0)
                    {
                        return status;
                    }

                    if (time.HasValue && endTime.HasValue && date.TimeOfDay.IsBetween(time.Value, endTime.Value))
                    {
                        return status;
                    }
                }
			}

			return AvailabilityStatus.Undefined;
		}
	}
}
