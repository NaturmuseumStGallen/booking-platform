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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BookingPlatform.Backend.Constants;
using BookingPlatform.Backend.Entities;
using BookingPlatform.Backend.Entities.RuleConfigurations;
using BookingPlatform.Constants;

namespace BookingPlatform.Models
{
	public class AdminSettingsModel
	{
		public AdminSettingsModel()
		{
			Events = new List<Event>();
			Recipients = new List<EmailRecipient>();
			Rules = new List<RuleConfiguration>();
			TextContent = new List<TextContent>();
			Times = new List<TimeData>();
		}

		public string EmailSubject { get; set; }
		public string EmailContent { get; set; }
		public string ConfirmationPageContent { get; set; }

		public IList<Event> Events { get; set; }
		public IList<EmailRecipient> Recipients { get; set; }
		public IList<RuleConfiguration> Rules { get; set; }
		public IList<TextContent> TextContent { get; set; }
		public IList<TimeData> Times { get; set; }

		public IEnumerable<SelectListItem> RuleTypes
		{
			get
			{
				foreach (RuleType type in Enum.GetValues(typeof(RuleType)))
				{
					yield return new SelectListItem { Text = Strings.Admin.GetRuleTypeName(type), Value = type.ToString() };
				}
			}
		}

		public IEnumerable<SelectListItem> DisplayDayListItems
		{
			get
			{
				yield return new SelectListItem { Text = Strings.Admin.Settings.DisplayAlways, Value = string.Empty };

				foreach (var day in Enumerable.Range(1, 6).Concat(Enumerable.Range(0, 1)).Cast<DayOfWeek>())
				{
					yield return new SelectListItem
					{
						Text = DateTimeFormatInfo.CurrentInfo.GetDayName(day),
						Value = day.ToString()
					};
				}
			}
		}

		public MvcHtmlString GetRuleDetails(RuleConfiguration rule)
		{
			switch (rule.Type)
			{
				case RuleType.DateRange:
					return DateRangeDetails(rule as DateRangeRuleConfiguration);
				case RuleType.EventGroup:
					return EventGroupDetails(rule as EventGroupRuleConfiguration);
				case RuleType.MinimumDate:
					return MinimumDateDetails(rule as MinimumDateRuleConfiguration);
				case RuleType.Weekly:
					return WeeklyDetails(rule as WeeklyRuleConfiguration);
				default:
					throw new InvalidOperationException(String.Format("Rule of type '{0}' not yet configured!", rule.Type));
			}
		}

		private MvcHtmlString DateRangeDetails(DateRangeRuleConfiguration config)
		{
			var builder = new StringBuilder();

			builder.AppendFormat("{0}: {1}<br />", Strings.Admin.RuleDetails.InputLabelStatus, Strings.Admin.GetStatusName(config.AvailabilityStatus));
			builder.AppendFormat("{0}: {1}<br />", Strings.Admin.RuleDetails.InputLabelStartDate, config.StartDate.ToShortDateString());
			builder.AppendFormat("{0}: {1}<br />", Strings.Admin.RuleDetails.InputLabelStartTime, config.StartTime.HasValue ? config.StartTime.Value.ToString("hh\\:mm") : "-");
			builder.AppendFormat("{0}: {1}<br />", Strings.Admin.RuleDetails.InputLabelEndDate, config.EndDate.HasValue ? config.EndDate.Value.ToShortDateString() : "-");
			builder.AppendFormat("{0}: {1}<br />", Strings.Admin.RuleDetails.InputLabelEndTime, config.EndTime.HasValue ? config.EndTime.Value.ToString("hh\\:mm") : "-");

			return new MvcHtmlString(builder.ToString());
		}

		private MvcHtmlString EventGroupDetails(EventGroupRuleConfiguration config)
		{
			var builder = new StringBuilder();

			foreach (var id in config.EventIds)
			{
				builder.AppendFormat("- {0}<br />", Events.First(e => e.Id == id).Name);
			}

			return new MvcHtmlString(builder.ToString());
		}

		private MvcHtmlString MinimumDateDetails(MinimumDateRuleConfiguration config)
		{
			var builder = new StringBuilder();

			builder.AppendFormat("{0}: {1}<br />", Strings.Admin.RuleDetails.InputLabelDate, config.Date.HasValue ? config.Date.Value.ToShortDateString() : "-");
			builder.AppendFormat("{0}: {1}<br />", Strings.Admin.RuleDetails.InputLabelDays, config.Days.HasValue ? config.Days.ToString() : "-");

			return new MvcHtmlString(builder.ToString());
		}

		private MvcHtmlString WeeklyDetails(WeeklyRuleConfiguration config)
		{
			var builder = new StringBuilder();

			builder.AppendFormat("{0}: {1}<br />", Strings.Admin.RuleDetails.InputLabelStatus, Strings.Admin.GetStatusName(config.AvailabilityStatus));
			builder.AppendFormat("{0}: {1}<br />", Strings.Admin.RuleDetails.InputLabelDayOfWeek, DateTimeFormatInfo.CurrentInfo.GetDayName(config.DayOfWeek));
			builder.AppendFormat("{0}: {1}<br />", Strings.Admin.RuleDetails.InputLabelTime, config.Time.HasValue ? config.Time.Value.ToString("hh\\:mm") : "-");
			builder.AppendFormat("{0}: {1}<br />", Strings.Admin.RuleDetails.InputLabelStartDate, config.StartDate.HasValue ? config.StartDate.Value.ToShortDateString() : "-");

			return new MvcHtmlString(builder.ToString());
		}
	}
}