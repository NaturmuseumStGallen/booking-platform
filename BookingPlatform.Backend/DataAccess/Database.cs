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

		public void DeactivateEvent(int id)
		{
			new DbEventDao().Deactivate(id);
			new DbRuleDao().RemoveFromEventGroups(id);
		}

		public void DeleteEmailRecipient(int id)
		{
			new DbEmailDao().Delete(id);
		}

		public void DeleteRule(int id)
		{
			new DbRuleDao().Delete(id);
		}

		public void DeleteTextContent(int id)
		{
			new DbTextContentDao().Delete(id);
		}

		public void DeleteTime(int id)
		{
			new DbTimeDao().Delete(id);
		}

		public IList<Event> GetActiveEvents()
		{
			return new DbEventDao().GetAllActive();
		}

		public int GetActiveEventsCount()
		{
			return new DbEventDao().GetActiveCount();
		}

		public Booking GetBookingBy(int id)
		{
			var booking = new DbBookingDao().GetBy(id);

			booking.Event = new DbEventDao().GetBy(booking.EventId.Value);

			return booking;
		}

		public IList<Booking> GetBookings(DateTime from, DateTime to)
		{
			var bookings = new DbBookingDao().GetBookings(from, to);
			var eventDao = new DbEventDao();

			foreach (var booking in bookings)
			{
				booking.Event = eventDao.GetBy(booking.EventId.Value);
			}

			return bookings;
		}

		public IList<EmailRecipient> GetEmailRecipients()
		{
			return new DbEmailDao().GetAll();
		}

		public int GetEmailRecipientsCount()
		{
			return new DbEmailDao().GetCount();
		}

		public Event GetEventBy(int id)
		{
			return new DbEventDao().GetBy(id);
		}

		public IList<Event> GetEventsBy(IList<int> ids)
		{
			return new DbEventDao().GetBy(ids);
		}

		public Booking GetNewestActiveBooking()
		{
			var booking = new DbBookingDao().GetNewestActive();

			if (booking != null)
			{
				booking.Event = new DbEventDao().GetBy(booking.EventId.Value);
			}

			return booking;
		}

		public int GetPendingBookingsCount()
		{
			return new DbBookingDao().GetPendingCount();
		}

		public Settings GetSettings()
		{
			return new DbSettingsDao().Get();
		}

		public IList<IRule> GetRules()
		{
			return new DbRuleDao().GetRules();
		}

		public int GetRuleCount()
		{
			return new DbRuleDao().GetTotalCount();
		}

		public RuleConfiguration GetRuleData(int id)
		{
			return new DbRuleDao().GetConfigurationBy(id);
		}

		public IList<RuleConfiguration> GetRuleData()
		{
			return new DbRuleDao().GetRuleConfigurations();
		}

		public IList<TextContent> GetTextContents()
		{
			return new DbTextContentDao().GetAll();
		}

		public IList<TimeSpan> GetTimes()
		{
			return new DbTimeDao().GetTimes();
		}

		public int GetTimesCount()
		{
			return new DbTimeDao().GetCount();
		}

		public IList<TimeData> GetTimeData()
		{
			return new DbTimeDao().GetTimeData();
		}

		public int GetTotalBookingCount()
		{
			return new DbBookingDao().GetTotalCount();
		}

		public IList<Booking> GetUpcomingBookingsFor(DateTime date)
		{
			var bookings = new DbBookingDao().GetUpcomingBookings(date);
			var eventDao = new DbEventDao();

			foreach (var booking in bookings)
			{
				booking.Event = eventDao.GetBy(booking.EventId.Value);
			}

			return bookings;
		}

		public bool IsValidBookingId(int id)
		{
			return new DbBookingDao().Exists(id);
		}

		public bool IsValidEventId(int id)
		{
			return new DbEventDao().Exists(id);
		}

		public bool IsValidRuleId(int id)
		{
			return new DbRuleDao().Exists(id);
		}

		public void SaveNew(Booking booking)
		{
			new DbBookingDao().SaveNew(booking);
		}

		public void SaveNew(Event @event)
		{
			new DbEventDao().SaveNew(@event);
		}

		public void SaveNew(RuleConfiguration ruleData)
		{
			new DbRuleDao().SaveNew(ruleData);
		}

		public void SaveNew(TextContent content)
		{
			new DbTextContentDao().SaveNew(content);
		}

		public void SaveNewEmailRecipient(string email)
		{
			new DbEmailDao().SaveNew(email);
		}

		public void SaveNewTime(TimeSpan time)
		{
			new DbTimeDao().SaveNew(time);
		}

		public void Update(Booking booking)
		{
			new DbBookingDao().Update(booking);
		}

		public void Update(Event @event)
		{
			new DbEventDao().Update(@event);
		}

		public void Update(RuleConfiguration ruleData)
		{
			new DbRuleDao().Update(ruleData);
		}

		public void UpdateBookingState(int id, bool isActive)
		{
			new DbBookingDao().UpdateState(id, isActive);
		}

		public void UpdateConfirmationPageContent(string content)
		{
			new DbSettingsDao().UpdateConfirmationPageContent(content);
		}

		public void UpdateEmailContent(string title, string content)
		{
			new DbSettingsDao().UpdateEmailContent(title, content);
		}

		public void UpdatePassword(string hash, string salt)
		{
			new DbSettingsDao().UpdatePassword(hash, salt);
		}
	}
}
