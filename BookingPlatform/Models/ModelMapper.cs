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
using BookingPlatform.Backend.DataAccess;
using BookingPlatform.Backend.Entities;

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

		public static void MapFromEntity(this AdminBookingDetailsModel model, Booking booking)
		{
			model.Address = booking.Address;
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
			switch (model.Type)
			{
				case RuleType.DateRange:
					(model as DateRangeRuleModel).MapFromEntity(rule);
					break;
				case RuleType.EventGroup:
					(model as EventGroupRuleModel).MapFromEntity(rule);
					break;
				case RuleType.MinimumDate:
					(model as MinimumDateRuleModel).MapFromEntity(rule);
					break;
				case RuleType.Weekly:
					(model as WeeklyRuleModel).MapFromEntity(rule);
					break;
				default:
					throw new InvalidOperationException(String.Format("Rule of type '{0}' not yet configured!", model.Type));
			}
		}

		public static void MapFromEntity(this DateRangeRuleModel model, RuleConfiguration rule)
		{

		}

		public static void MapFromEntity(this EventGroupRuleModel model, RuleConfiguration rule)
		{

		}

		public static void MapFromEntity(this MinimumDateRuleModel model, RuleConfiguration rule)
		{

		}

		public static void MapFromEntity(this WeeklyRuleModel model, RuleConfiguration rule)
		{

		}

		public static void MapFromEntity(this AdminSettingsModel model, Settings settings)
		{
			model.EmailTitle = settings.EmailTitle;
			model.HtmlContent = settings.HtmlEmailContent;
			model.PlaintextContent = settings.PlaintextEmailContent;
		}

		public static void MapToEntity(this AdminBookingDetailsModel model, Booking booking)
		{
			booking.Address = model.Address;
			booking.Date = DateTime.Parse(model.Date + " " + model.Time);
			booking.Email = model.Email;
			booking.EventId = model.EventId;
			booking.FirstName = model.FirstName;
			booking.Grade = model.Grade;
			booking.Id = model.Id;
			booking.IsActive = model.IsActive;
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
			@event.Name = model.Name;
			@event.ColorComponentBlue = model.Blue.Value;
			@event.ColorComponentGreen = model.Green.Value;
			@event.ColorComponentRed = model.Red.Value;
		}

		public static void MapToEntity(this AdminRuleDetailsModel model, RuleConfiguration rule)
		{

		}

		public static void MapToEntity(this DateRangeRuleModel model, RuleConfiguration rule)
		{

		}

		public static void MapToEntity(this EventGroupRuleModel model, RuleConfiguration rule)
		{

		}

		public static void MapToEntity(this MinimumDateRuleModel model, RuleConfiguration rule)
		{

		}

		public static void MapToEntity(this WeeklyRuleModel model, RuleConfiguration rule)
		{

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