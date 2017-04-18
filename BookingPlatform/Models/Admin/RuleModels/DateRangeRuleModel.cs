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
using System.Linq;
using System.Web.Mvc;
using BookingPlatform.Backend.Constants;
using BookingPlatform.Constants;

namespace BookingPlatform.Models
{
	public class DateRangeRuleModel : AdminRuleDetailsModel, IValidatableObject
	{
		public DateRangeRuleModel()
		{
			Type = RuleType.DateRange;
		}

		[RegularExpression("^((0[1-9])|([1-2][0-9])|(3[0-1])).((0[1-9])|(1[0-2])).20[1-5][0-9]$", ErrorMessage = Strings.Admin.RuleDetails.InputErrorDate)]
		public string EndDate { get; set; }

		[RegularExpression("^((0[0-9])|(1[0-9])|(2[0-3])):((0[0-9])|([1-5][0-9]))$", ErrorMessage = Strings.Admin.RuleDetails.InputErrorTime)]
		public string EndTime { get; set; }

		[Required(ErrorMessage = Strings.Admin.RuleDetails.InputErrorDate)]
		[RegularExpression("^((0[1-9])|([1-2][0-9])|(3[0-1])).((0[1-9])|(1[0-2])).20[1-5][0-9]$", ErrorMessage = Strings.Admin.RuleDetails.InputErrorDate)]
		public string StartDate { get; set; }

		[RegularExpression("^((0[0-9])|(1[0-9])|(2[0-3])):((0[0-9])|([1-5][0-9]))$", ErrorMessage = Strings.Admin.RuleDetails.InputErrorTime)]
		public string StartTime { get; set; }

		[Required(ErrorMessage = Strings.Admin.RuleDetails.InputErrorStatus)]
		public AvailabilityStatus? Status { get; set; }

		public int? Id { get; set; }

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
			DateTime startDate, endDate;
			TimeSpan startTime, endTime;
			var results = new List<ValidationResult>();

			if (!DateTime.TryParse(StartDate, out startDate))
			{
				results.Add(new ValidationResult(Strings.Admin.RuleDetails.InputErrorDate, new[] { nameof(StartDate) }));
			}

			if (!String.IsNullOrEmpty(StartTime) && !TimeSpan.TryParse(StartTime, out startTime))
			{
				results.Add(new ValidationResult(Strings.Admin.RuleDetails.InputErrorTime, new[] { nameof(StartTime) }));
			}

			if (!String.IsNullOrEmpty(EndDate) && !DateTime.TryParse(StartDate, out endDate))
			{
				results.Add(new ValidationResult(Strings.Admin.RuleDetails.InputErrorDate, new[] { nameof(EndDate) }));
			}

			if (!String.IsNullOrEmpty(EndTime) && !TimeSpan.TryParse(StartTime, out endTime))
			{
				results.Add(new ValidationResult(Strings.Admin.RuleDetails.InputErrorTime, new[] { nameof(EndTime) }));
			}

			if (results.Any())
			{
				results.Add(ValidationResult.Success);
			}

			return results;
		}
	}
}