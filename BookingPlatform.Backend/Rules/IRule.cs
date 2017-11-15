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
using System.Collections.Generic;

namespace BookingPlatform.Backend.Rules
{
	public interface IRule
	{
		/// <summary>
		/// Defines the type of this rule, used for database persistence and in the presentation layer.
		/// </summary>
		RuleType Type { get; }

	}

    public interface IStandardRule : IRule
    {
        /// <summary>
        /// Returns the availability status according to the specified date and event. Should return
        /// <c>AvailabilityStatus.Undefined</c> if the rule is not relevant for the given parameters.
        /// </summary>
        AvailabilityStatus GetStatus(DateTime date, Event @event);
    }
}
