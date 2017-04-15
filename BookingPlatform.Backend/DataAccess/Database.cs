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
using BookingPlatform.Backend.Rules;

namespace BookingPlatform.Backend.DataAccess
{
	public class Database : IBookingProvider, IRuleProvider, ITimeProvider
	{
		public static Database Instance
		{
			get { return new Database(); }
		}

		public IList<Event> GetActiveEvents()
		{
			return new DbEventProvider().GetAllActive();
		}

		public IList<Booking> GetBookings(DateTime from, DateTime to)
		{
			return new DbBookingProvider().GetBookings(from, to);
		}

		public IList<EmailRecipient> GetEmailRecipients()
		{
			return new DbEmailProvider().GetAll();
		}

		public Event GetEventBy(int id)
		{
			return new DbEventProvider().GetById(id);
		}

		public IList<EventGroup> GetEventGroups()
		{
			return new DbEventGroupProvider().GetAll();
		}

		public Settings GetGlobalSettings()
		{
			return new Settings { HtmlEmailContent = "Some content goes here" };
		}

		public IList<IRule> GetRules(DateTime from, DateTime to)
		{
			return new DbRuleProvider().GetRules(from, to);
		}

		public RuleData GetRuleData(int id)
		{
			return new DbRuleProvider().GetRuleData(id);
		}

		public IList<RuleData> GetRuleData()
		{
			return new DbRuleProvider().GetRuleData();
		}

		public IList<TimeSpan> GetTimes()
		{
			return new DbTimeProvider().GetTimes();
		}

		public IList<TimeData> GetTimeData()
		{
			return new DbTimeProvider().GetTimeData();
		}

		public bool IsValidEventId(int id)
		{
			// TODO!
			return id >= 0;
		}

		public bool IsValidRuleId(int id)
		{
			// TODO
			return id >= 0;
		}

		public void SaveEmailRecipient(string email)
		{
			// TODO!
		}
	}
}
