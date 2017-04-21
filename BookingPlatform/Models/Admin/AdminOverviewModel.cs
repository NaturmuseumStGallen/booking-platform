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
using BookingPlatform.Backend.Entities;
using BookingPlatform.Constants;

namespace BookingPlatform.Models
{
	public class AdminOverviewModel
	{
		public AdminOverviewModel()
		{
			UpcomingBookings = new List<Booking>();
			Warnings = new List<Warning>();
		}

		public int ActiveEventsCount { get; set; }
		public Booking NewestBooking { get; set; }
		public int PendingBookingsCount { get; set; }
		public int RulesCount { get; set; }
		public int TotalBookingCount { get; set; }

		public IList<Booking> UpcomingBookings { get; set; }
		public IList<Warning> Warnings { get; set; }

		public bool HasNewestBooking
		{
			get { return NewestBooking != null; }
		}

		public string GetNewestBookingInfo()
		{
			if (HasNewestBooking)
			{
				var eventName = NewestBooking.Event.Name;
				var date = NewestBooking.Date.ToLongDateString();
				var person = NewestBooking.FirstName + " " + NewestBooking.LastName;
				var school = NewestBooking.School;
				var town = NewestBooking.Town;
				var canton = NewestBooking.Canton.Length == 2 ? " " + NewestBooking.Canton : string.Empty;

				return String.Format("{0} @ {1} ({2}, {3} - {4}{5})", eventName, date, person, school, town, canton);
			}

			return "-";
		}

		public string GetWarningText(Warning warning)
		{
			switch (warning)
			{
				case Warning.NO_CONFIRMATION_PAGE_CONTENT_CONFIGURED:
					return Strings.Admin.Overview.WarningNoConfirmationContent;
				case Warning.NO_EMAIL_CONTENT_CONFIGURED:
					return Strings.Admin.Overview.WarningNoEmailContent;
				case Warning.NO_EMAIL_RECIPIENTS_CONFIGURED:
					return Strings.Admin.Overview.WarningNoEmailRecipients;
				case Warning.NO_EVENTS_CONFIGURED:
					return Strings.Admin.Overview.WarningNoEvents;
				case Warning.NO_TIMES_CONFIGURED:
					return Strings.Admin.Overview.WarningNoTimes;
				default:
					throw new InvalidOperationException(String.Format("Warning of type '{0}' not configured!", warning));
			}
		}

		public enum Warning
		{
			NO_CONFIRMATION_PAGE_CONTENT_CONFIGURED,
			NO_EMAIL_CONTENT_CONFIGURED,
			NO_EMAIL_RECIPIENTS_CONFIGURED,
			NO_EVENTS_CONFIGURED,
			NO_TIMES_CONFIGURED
		}
	}
}