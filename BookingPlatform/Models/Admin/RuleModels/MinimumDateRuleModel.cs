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
using BookingPlatform.Backend.Constants;
using BookingPlatform.Constants;

namespace BookingPlatform.Models
{
	public class MinimumDateRuleModel : AdminRuleDetailsModel, IValidatableObject
	{
		public MinimumDateRuleModel()
		{
			Type = RuleType.MinimumDate;
		}

		[RegularExpression("^((0[1-9])|([1-2][0-9])|(3[0-1])).((0[1-9])|(1[0-2])).20[1-5][0-9]$", ErrorMessage = Strings.Admin.RuleDetails.InputErrorDate)]
		public string Date { get; set; }

		[Range(0, int.MaxValue, ErrorMessage = Strings.Admin.RuleDetails.InputErrorDays)]
		public int? Days { get; set; }

		public int? Id { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			DateTime date;
			var results = new List<ValidationResult>();

			if (!Days.HasValue && !DateTime.TryParse(Date, out date))
			{
				results.Add(new ValidationResult(Strings.Admin.RuleDetails.InputErrorDate, new[] { nameof(Date) }));
			}

			if (results.Any())
			{
				results.Add(ValidationResult.Success);
			}

			return results;
		}
	}
}