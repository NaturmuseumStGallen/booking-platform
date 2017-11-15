/*
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
using BookingPlatform.Backend.Entities;
using BookingPlatform.Backend.Scheduling;

namespace BookingPlatform.Backend.Rules
{
    public class MultipleBookingRule : IRule
    {
        public RuleType Type => RuleType.MultipleBooking;
        public int EventId { get; }
        public int NumberOfParallelBookings { get; }

        private Dictionary<DateTime, int> numberOfBookingsByDateTime;

        public MultipleBookingRule(int eventId, int numberOfParallelBookings)
        {
            this.EventId = eventId;
            this.NumberOfParallelBookings = numberOfParallelBookings;
            this.numberOfBookingsByDateTime = new Dictionary<DateTime, int>();
        }

        public AvailabilityStatus GetStatus(DateTime dateTime, Event @event, IList<Booking> bookings)
        {
            var status = new List<AvailabilityStatus>();

            foreach (var booking in bookings)
            {
                if (booking.IsActive && booking.Event.Id == @event.Id && booking.Date.IsSameDateAndTimeAs(dateTime))
                {
                    if (!numberOfBookingsByDateTime.ContainsKey(dateTime))
                        numberOfBookingsByDateTime.Add(dateTime, 1);

                    if (numberOfBookingsByDateTime[dateTime] < NumberOfParallelBookings)
                    {
                        numberOfBookingsByDateTime[dateTime]++;
                        continue;
                    }

                    return AvailabilityStatus.Booked;
                }
            }

            return AvailabilityStatus.Undefined;
        }

    }
}
