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
using System.Linq;
using BookingPlatform.Backend.Constants;
using BookingPlatform.Backend.DataAccess;
using BookingPlatform.Backend.Entities;
using BookingPlatform.Backend.Entities.RuleConfigurations;
using BookingPlatform.Backend.Scheduling;
using BookingPlatform.Utilities;

namespace BookingPlatform.Models
{
	public static class ModelMapper
	{
		public static void InitializeFor(this AdminRuleDetailsModel model, RuleType type)
		{
			switch (model.Type)
			{
				case RuleType.DateRange:
				case RuleType.MinimumDate:
				case RuleType.Weekly:
					// Nothing to do here so far...
					break;
				case RuleType.EventGroup:
					(model as EventGroupRuleModel).AvailableEvents = Database.Instance.GetActiveEvents();
					break;
				default:
					throw new InvalidOperationException(String.Format("Rule of type '{0}' not yet configured!", model.Type));
			}
		}

		public static void Initialize(this AdminOverviewModel model)
		{
			var settings = Database.Instance.GetSettings();

			model.ActiveEventsCount = Database.Instance.GetActiveEventsCount();
			model.NewestBooking = Database.Instance.GetNewestActiveBooking();
			model.PendingBookingsCount = Database.Instance.GetPendingBookingsCount();
			model.RulesCount = Database.Instance.GetRuleCount();
			model.TotalBookingCount = Database.Instance.GetTotalBookingCount();
			model.UpcomingBookings = Database.Instance.GetUpcomingBookingsFor(DateTime.Today);

			if (Database.Instance.GetEmailRecipientsCount() == 0)
			{
				model.Warnings.Add(AdminOverviewModel.Warning.NO_EMAIL_RECIPIENTS_CONFIGURED);
			}

			if (model.ActiveEventsCount == 0)
			{
				model.Warnings.Add(AdminOverviewModel.Warning.NO_EVENTS_CONFIGURED);
			}

			if (Database.Instance.GetTimesCount() == 0)
			{
				model.Warnings.Add(AdminOverviewModel.Warning.NO_TIMES_CONFIGURED);
			}

			if (!ValidationUtility.AreNotNullOrWhitespace(settings.EmailContent, settings.EmailTitle))
			{
				model.Warnings.Add(AdminOverviewModel.Warning.NO_EMAIL_CONTENT_CONFIGURED);
			}

			if (!ValidationUtility.AreNotNullOrWhitespace(settings.ConfirmationPageContent))
			{
				model.Warnings.Add(AdminOverviewModel.Warning.NO_CONFIRMATION_PAGE_CONTENT_CONFIGURED);
			}
		}

		public static void Initialize(this AdminSettingsModel model)
		{
			model.Events = Database.Instance.GetActiveEvents();
			model.Recipients = Database.Instance.GetEmailRecipients();
			model.Rules = Database.Instance.GetRuleData();
			model.TextContent = Database.Instance.GetTextContent();
			model.Times = Database.Instance.GetTimeData();
		}

		public static void MapFromEntity(this AdminBookingDetailsModel model, Booking booking)
		{
			model.Address = booking.Address;
			model.Canton = booking.Canton;
			model.Date = booking.Date.ToString("dd.MM.yyyy");
			model.Email = booking.Email;
			model.EventId = booking.Event.Id;
			model.FirstName = booking.FirstName;
			model.Grade = booking.Grade;
			model.Id = booking.Id;
			model.IsActive = booking.IsActive;
			model.LastName = booking.LastName;
			model.Notes = booking.Notes;
			model.NumberOfKids = booking.NumberOfKids;
			model.Phone = booking.Phone;
			model.School = booking.School;
			model.Time = booking.Date.TimeOfDay.ToString("hh\\:mm");
			model.Town = booking.Town;
			model.ZipCode = booking.ZipCode;
		}

		public static void MapFromEntity(this AdminEventDetailsModel model, Event @event)
		{
			model.Id = @event.Id;
			model.Name = @event.Name;
			model.Blue = @event.ColorComponentBlue;
			model.Green = @event.ColorComponentGreen;
			model.Red = @event.ColorComponentRed;
		}

		public static void MapFromEntity(this AdminRuleDetailsModel model, RuleConfiguration rule)
		{
			model.RuleId = rule.RuleId;
			model.Name = rule.Name;
			model.Type = rule.Type;

			switch (model.Type)
			{
				case RuleType.DateRange:
					(model as DateRangeRuleModel).MapFromEntity(rule as DateRangeRuleConfiguration);
					break;
				case RuleType.EventGroup:
					(model as EventGroupRuleModel).MapFromEntity(rule as EventGroupRuleConfiguration);
					break;
				case RuleType.MinimumDate:
					(model as MinimumDateRuleModel).MapFromEntity(rule as MinimumDateRuleConfiguration);
					break;
				case RuleType.Weekly:
					(model as WeeklyRuleModel).MapFromEntity(rule as WeeklyRuleConfiguration);
					break;
				default:
					throw new InvalidOperationException(String.Format("Rule of type '{0}' not yet configured!", model.Type));
			}
		}

		public static void MapFromEntity(this DateRangeRuleModel model, DateRangeRuleConfiguration rule)
		{
			model.Id = rule.Id;
			model.EndDate = rule.EndDate.ToString("dd.MM.yyyy");
			model.EndTime = rule.EndTime.ToString("hh\\:mm");
			model.StartDate = rule.StartDate.ToString("dd.MM.yyyy");
			model.StartTime = rule.StartTime.ToString("hh\\:mm");
			model.Status = rule.AvailabilityStatus;
		}

		public static void MapFromEntity(this EventGroupRuleModel model, EventGroupRuleConfiguration rule)
		{
			model.Id = rule.Id;
			model.EventIds = rule.EventIds.Select(i => i.ToString()).ToArray();
		}

		public static void MapFromEntity(this MinimumDateRuleModel model, MinimumDateRuleConfiguration rule)
		{
			model.Id = rule.Id;
			model.Date = rule.Date.ToString("dd.MM.yyyy");
			model.Days = rule.Days;
		}

		public static void MapFromEntity(this WeeklyRuleModel model, WeeklyRuleConfiguration rule)
		{
			model.Id = rule.Id;
			model.Status = rule.AvailabilityStatus;
			model.Day = rule.DayOfWeek;
			model.StartDate = rule.StartDate.ToString("dd.MM.yyyy");
			model.Time = rule.Time.ToString("hh\\:mm");
		}

		public static void MapFromEntity(this AdminSettingsModel model, Settings settings)
		{
			model.EmailTitle = settings.EmailTitle;
			model.EmailContent = settings.EmailContent;
			model.ConfirmationPageContent = settings.ConfirmationPageContent;
		}

		public static void MapToEntity(this AdminBookingDetailsModel model, Booking booking)
		{
			booking.Address = model.Address;
			booking.Canton = model.Canton;
			booking.Date = DateTime.Parse(model.Date + " " + model.Time);
			booking.Email = model.Email;
			booking.EventId = model.EventId;
			booking.FirstName = model.FirstName;
			booking.Grade = model.Grade;
			booking.Id = model.Id;
			booking.LastName = model.LastName;
			booking.Notes = model.Notes;
			booking.NumberOfKids = model.NumberOfKids.Value;
			booking.Phone = model.Phone;
			booking.School = model.School;
			booking.Town = model.Town;
			booking.ZipCode = model.ZipCode;
		}

		public static void MapToEntity(this AdminEventDetailsModel model, Event @event)
		{
			@event.Id = model.Id;
			@event.IsActive = true;
			@event.Name = model.Name;
			@event.ColorComponentBlue = model.Blue.Value;
			@event.ColorComponentGreen = model.Green.Value;
			@event.ColorComponentRed = model.Red.Value;
		}

		public static void MapToEntity(this AdminRuleDetailsModel model, RuleConfiguration rule)
		{
			if (model.RuleId.HasValue)
			{
				rule.RuleId = model.RuleId.Value;
			}

			rule.Name = model.Name;
			rule.Type = model.Type.Value;

			switch (model.Type)
			{
				case RuleType.DateRange:
					(model as DateRangeRuleModel).MapToEntity(rule as DateRangeRuleConfiguration);
					break;
				case RuleType.EventGroup:
					(model as EventGroupRuleModel).MapToEntity(rule as EventGroupRuleConfiguration);
					break;
				case RuleType.MinimumDate:
					(model as MinimumDateRuleModel).MapToEntity(rule as MinimumDateRuleConfiguration);
					break;
				case RuleType.Weekly:
					(model as WeeklyRuleModel).MapToEntity(rule as WeeklyRuleConfiguration);
					break;
				default:
					throw new InvalidOperationException(String.Format("Rule of type '{0}' not yet configured!", model.Type));
			}
		}

		public static void MapToEntity(this DateRangeRuleModel model, DateRangeRuleConfiguration rule)
		{
			if (model.Id.HasValue)
			{
				rule.Id = model.Id.Value;
			}

			rule.AvailabilityStatus = model.Status.Value;
			rule.EndDate = DateTimeUtility.NullableDateTimeFor(model.EndDate);
			rule.EndTime = DateTimeUtility.NullableTimeSpanFor(model.EndTime);
			rule.StartDate = DateTime.Parse(model.StartDate);
			rule.StartTime = DateTimeUtility.NullableTimeSpanFor(model.StartTime);
		}

		public static void MapToEntity(this EventGroupRuleModel model, EventGroupRuleConfiguration rule)
		{
			if (model.Id.HasValue)
			{
				rule.Id = model.Id.Value;
			}

			rule.EventIds = model.EventIds.Select(i => int.Parse(i)).ToList();
		}

		public static void MapToEntity(this MinimumDateRuleModel model, MinimumDateRuleConfiguration rule)
		{
			if (model.Id.HasValue)
			{
				rule.Id = model.Id.Value;
			}

			rule.Date = DateTimeUtility.NullableDateTimeFor(model.Date);
			rule.Days = model.Days;
		}

		public static void MapToEntity(this WeeklyRuleModel model, WeeklyRuleConfiguration rule)
		{
			if (model.Id.HasValue)
			{
				rule.Id = model.Id.Value;
			}

			rule.AvailabilityStatus = model.Status.Value;
			rule.DayOfWeek = model.Day.Value;
			rule.StartDate = DateTimeUtility.NullableDateTimeFor(model.StartDate);
			rule.Time = DateTimeUtility.NullableTimeSpanFor(model.Time);
		}

		public static void MapToEntity(this BookingModel model, Booking booking)
		{
			booking.Address = model.Address;
			booking.Canton = model.Canton;
			booking.Date = DateTimeUtility.NewFor(model.DateTicks.Value);
			booking.Email = model.Email;
			booking.EventId = model.EventId;
			booking.Event = Database.Instance.GetEventBy(model.EventId.Value);
			booking.FirstName = model.FirstName;
			booking.Grade = model.Grade;
			booking.IsActive = true;
			booking.LastName = model.LastName;
			booking.Notes = model.Notes;
			booking.NumberOfKids = model.NumberOfKids.Value;
			booking.Phone = model.Phone;
			booking.School = model.School;
			booking.Town = model.Town;
			booking.ZipCode = model.ZipCode;
		}

		public static RuleConfiguration NewEntityFor(RuleType type)
		{
			switch (type)
			{
				case RuleType.DateRange:
					return new DateRangeRuleConfiguration();
				case RuleType.EventGroup:
					return new EventGroupRuleConfiguration();
				case RuleType.MinimumDate:
					return new MinimumDateRuleConfiguration();
				case RuleType.Weekly:
					return new WeeklyRuleConfiguration();
				default:
					throw new InvalidOperationException(String.Format("Rule of type '{0}' not yet configured!", type));
			}

		}

		public static AdminRuleDetailsModel NewModelFor(RuleType type)
		{
			switch (type)
			{
				case RuleType.DateRange:
					return new DateRangeRuleModel();
				case RuleType.EventGroup:
					return new EventGroupRuleModel();
				case RuleType.MinimumDate:
					return new MinimumDateRuleModel();
				case RuleType.Weekly:
					return new WeeklyRuleModel();
				default:
					throw new InvalidOperationException(String.Format("Rule of type '{0}' not yet configured!", type));
			}
		}
	}
}