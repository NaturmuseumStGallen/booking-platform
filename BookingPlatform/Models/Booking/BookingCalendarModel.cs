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
using System.Linq;
using BookingPlatform.Backend.Entities;

namespace BookingPlatform.Models
{
	public class BookingCalendarModel
	{
		public BookingCalendarModel()
		{
			Dates = new List<BookingDate>();
		}

		public bool CanNavigateToPreviousMonth { get; set; }
		public bool CanNavigateToPreviousWeek { get; set; }
		public long CurrentDateTicks { get; set; }
		public bool ShowEventSelectionMessage { get; set; }

		public IList<BookingDate> Dates { get; set; }

		public IEnumerable<DateTime> Days
		{
			get { return Dates.GroupBy(d => d.Date.Date).Select(g => g.Key).OrderBy(d => d).ToList(); }
		}
		public IEnumerable<TimeSpan> Times
		{
			get { return Dates.GroupBy(d => d.Date.TimeOfDay).Select(g => g.Key).OrderBy(t => t).ToList(); }
		}
	}
}