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
using BookingPlatform.Backend.Entities;
using BookingPlatform.Backend.Scheduling;
using BookingPlatform.Constants;

namespace BookingPlatform.Models
{
	public class EventDurationRuleModel : AdminRuleDetailsModel, IValidatableObject
	{
		public EventDurationRuleModel()
		{
			AvailableEvents = new List<Event>();
		}

		public override RuleType Type
		{
			get { return RuleType.EventDuration; }
		}

		[Required(ErrorMessage = Strings.Admin.RuleDetails.InputErrorEvent)]
		public int? EventId { get; set; }

		[Required(ErrorMessage = Strings.Admin.RuleDetails.InputErrorDate)]
		[RegularExpression("^((0[1-9])|([1-2][0-9])|(3[0-1])).((0[1-9])|(1[0-2])).20[1-5][0-9]$", ErrorMessage = Strings.Admin.RuleDetails.InputErrorDate)]
		public string StartDate { get; set; }

		[Required(ErrorMessage = Strings.Admin.RuleDetails.InputErrorDate)]
		[RegularExpression("^((0[1-9])|([1-2][0-9])|(3[0-1])).((0[1-9])|(1[0-2])).20[1-5][0-9]$", ErrorMessage = Strings.Admin.RuleDetails.InputErrorDate)]
		public string EndDate { get; set; }

		public int? Id { get; set; }
		public IList<Event> AvailableEvents { get; set; }

		public IEnumerable<SelectListItem> EventListItems
		{
			get
			{
				foreach (var @event in AvailableEvents)
				{
					yield return new SelectListItem { Text = @event.Name,  Value = @event.Id.ToString(), Selected = @event.Id == EventId };
				}
			}
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			DateTime startDate, endDate;
			var results = new List<ValidationResult>();

			if (!DateTime.TryParse(StartDate, out startDate))
			{
				results.Add(new ValidationResult(Strings.Admin.RuleDetails.InputErrorDate, new[] { nameof(StartDate) }));
			}

			if (!DateTime.TryParse(EndDate, out endDate))
			{
				results.Add(new ValidationResult(Strings.Admin.RuleDetails.InputErrorDate, new[] { nameof(EndDate) }));
			}

			if (!results.Any() && startDate.IsBiggerThan(endDate))
			{
				results.Add(new ValidationResult(Strings.Admin.RuleDetails.InputErrorInvalidDateRange, new[] { nameof(EndDate) }));
			}

			if (!results.Any())
			{
				results.Add(ValidationResult.Success);
			}

			return results;
		}
	}
}