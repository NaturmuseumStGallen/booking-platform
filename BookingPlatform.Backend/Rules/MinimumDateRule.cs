﻿/*
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
	public class MinimumDateRule : IStandardRule
    {
		private DateTime minimum;

		public RuleType Type
		{
			get { return RuleType.MinimumDate; }
		}

		/// <summary>
		/// Defines a rule which marks all dates prior to the specified date as not bookable.
		/// </summary>
		public MinimumDateRule(DateTime minimum)
		{
			this.minimum = minimum;
		}

		/// <summary>
		/// Defines a rule which marks all dates prior to today + the specified offset as not bookable.
		/// E.g. if the offset is 7 and today is Monday, events are not bookable until Monday next week.
		/// </summary>
		public MinimumDateRule(int offset)
		{
			minimum = DateTime.Today.AddDays(offset);
		}

		public AvailabilityStatus GetStatus(DateTime date, Event @event)
		{
			if (date.IsSmallerThan(minimum))
			{
				return AvailabilityStatus.NotBookable;
			}

			return AvailabilityStatus.Undefined;
		}
	}
}
