﻿/*
 * Copyright (C) 2017 Naturmuseum St. Gallen
 *  > https://github.com/NaturmuseumStGallen
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

namespace BookingPlatform.Backend.Rules
{
    public class BookingTimeOverrideRule : IRule
    {
        public RuleType Type => RuleType.BookingTimeOverride;

        public IList<TimeSpan> OverrideBookingTimes { get;}

        public int EventId { get; }

        public BookingTimeOverrideRule(int eventId, IList<TimeSpan> overrideBookingTimes)
        {
            EventId = eventId;
            OverrideBookingTimes = overrideBookingTimes;
        }
    }
}
