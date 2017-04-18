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
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using BookingPlatform.Backend.Constants;
using BookingPlatform.Constants;

namespace BookingPlatform.Models
{
	public class WeeklyRuleModel : AdminRuleDetailsModel, IValidatableObject
	{
		public WeeklyRuleModel()
		{
			Type = RuleType.Weekly;
		}

		[Required(ErrorMessage = Strings.Admin.RuleDetails.InputErrorDayOfWeek)]
		public DayOfWeek? Day { get; set; }

		[Required(ErrorMessage = Strings.Admin.RuleDetails.InputErrorStatus)]
		public AvailabilityStatus? Status { get; set; }

		[RegularExpression("^((0[0-9])|(1[0-9])|(2[0-3])):((0[0-9])|([1-5][0-9]))$", ErrorMessage = Strings.Admin.RuleDetails.InputErrorTime)]
		public string Time { get; set; }

		[RegularExpression("^((0[1-9])|([1-2][0-9])|(3[0-1])).((0[1-9])|(1[0-2])).20[1-5][0-9]$", ErrorMessage = Strings.Admin.RuleDetails.InputErrorDate)]
		public string StartDate { get; set; }

		public int? Id { get; set; }

		public IEnumerable<SelectListItem> DayOfWeekListItems
		{
			get
			{
				foreach (var day in Enumerable.Range(1, 6).Concat(Enumerable.Range(0, 1)).Cast<DayOfWeek>())
				{
					yield return new SelectListItem
					{
						Text = DateTimeFormatInfo.CurrentInfo.GetDayName(day),
						Value = day.ToString(),
						Selected = Day == day
					};
				}
			}
		}

		public IEnumerable<SelectListItem> StatusListItems
		{
			get
			{
				foreach (AvailabilityStatus status in Enum.GetValues(typeof(AvailabilityStatus)))
				{
					if (status != AvailabilityStatus.Undefined)
					{
						yield return new SelectListItem
						{
							Text = Strings.Admin.GetStatusName(status),
							Value = status.ToString(),
							Selected = Status == status
						};
					}
				}
			}
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			DateTime startDate;
			TimeSpan time;
			var results = new List<ValidationResult>();

			if (!String.IsNullOrEmpty(StartDate) && !DateTime.TryParse(StartDate, out startDate))
			{
				results.Add(new ValidationResult(Strings.Admin.RuleDetails.InputErrorDate, new[] { nameof(StartDate) }));
			}

			if (!String.IsNullOrEmpty(Time) && !TimeSpan.TryParse(Time, out time))
			{
				results.Add(new ValidationResult(Strings.Admin.RuleDetails.InputErrorTime, new[] { nameof(Time) }));
			}

			if (results.Any())
			{
				results.Add(ValidationResult.Success);
			}

			return results;
		}
	}
}