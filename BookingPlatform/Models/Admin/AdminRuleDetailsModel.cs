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
using BookingPlatform.Utilities;

namespace BookingPlatform.Models
{
	[ModelBinder(typeof(RuleModelBinder))]
	public abstract class AdminRuleDetailsModel
	{
		[Required(ErrorMessage = Strings.Admin.RuleDetails.InputErrorName)]
		[MaxLength(100, ErrorMessage = Strings.Admin.RuleDetails.InputErrorMaxLength100)]
		public string Name { get; set; }

		public int? RuleId { get; set; }
		public abstract RuleType Type { get; }

		public bool IsNew
		{
			get { return !RuleId.HasValue; }
		}

		public string GetDescription()
		{
			switch (Type)
			{
				case RuleType.DateRange:
					return Strings.Admin.RuleDetails.Descriptions.DateRangeRule;
				case RuleType.EventDuration:
					return Strings.Admin.RuleDetails.Descriptions.EventDurationRule;
				case RuleType.EventGroup:
					return Strings.Admin.RuleDetails.Descriptions.EventGroupRule;
				case RuleType.MinimumDate:
					return Strings.Admin.RuleDetails.Descriptions.MinimumDateRule;
				case RuleType.Weekly:
					return Strings.Admin.RuleDetails.Descriptions.WeeklyRule;
				default:
					throw new InvalidOperationException(String.Format("Rule of type '{0}' not yet configured!", Type));
			}
		}

		public IEnumerable<KeyValuePair<string, string>> GetConfigurationOptions()
		{
			switch (Type)
			{
				case RuleType.DateRange:
					return Strings.Admin.RuleDetails.Descriptions.DateRangeOptions;
				case RuleType.MinimumDate:
					return Strings.Admin.RuleDetails.Descriptions.MinimumDateOptions;
				case RuleType.Weekly:
					return Strings.Admin.RuleDetails.Descriptions.WeeklyOptions;
				case RuleType.EventDuration:
				case RuleType.EventGroup:
					return Enumerable.Empty<KeyValuePair<string, string>>();
				default:
					throw new InvalidOperationException(String.Format("Rule of type '{0}' not yet configured!", Type));
			}
		}
	}
}