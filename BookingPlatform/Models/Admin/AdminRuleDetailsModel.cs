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
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BookingPlatform.Backend.Constants;
using BookingPlatform.Constants;
using BookingPlatform.Utilities;

namespace BookingPlatform.Models
{
	[ModelBinder(typeof(RuleModelBinder))]
	public class AdminRuleDetailsModel
	{
		[Required(ErrorMessage = Strings.Admin.RuleDetails.InputErrorName)]
		[MaxLength(100, ErrorMessage = Strings.Admin.RuleDetails.InputErrorMaxLength100)]
		public string Name { get; set; }

		public int? RuleId { get; set; }
		public RuleType? Type { get; set; }

		public bool IsNew
		{
			get { return !RuleId.HasValue; }
		}

		public MvcHtmlString GetDescription()
		{
			switch (Type)
			{
				case RuleType.DateRange:
					return Strings.Admin.RuleDetails.Descriptions.DateRangeRule;
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
	}
}