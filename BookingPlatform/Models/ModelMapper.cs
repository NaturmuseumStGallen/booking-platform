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
using BookingPlatform.Backend.Entities;

namespace BookingPlatform.Models
{
	public static class ModelMapper
	{
		public static void MapFromEntity(this AdminRuleDetailsModel model, RuleData rule)
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

		public static void MapFromEntity(this AdminSettingsModel model, Settings settings)
		{
			model.HtmlContent = settings.HtmlEmailContent;
			model.Password = settings.Password;
			model.PlaintextContent = settings.PlaintextEmailContent;
		}

		public static void MapToEntity(this AdminRuleDetailsModel model, RuleData rule)
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

		private static void MapFromEntity(this DateRangeRuleModel model, RuleData rule)
		{

		}

		private static void MapFromEntity(this EventGroupRuleModel model, RuleData rule)
		{

		}

		private static void MapFromEntity(this MinimumDateRuleModel model, RuleData rule)
		{

		}

		private static void MapFromEntity(this WeeklyRuleModel model, RuleData rule)
		{

		}

		private static void MapToEntity(this DateRangeRuleModel model, RuleData rule)
		{

		}

		private static void MapToEntity(this EventGroupRuleModel model, RuleData rule)
		{

		}

		private static void MapToEntity(this MinimumDateRuleModel model, RuleData rule)
		{

		}

		private static void MapToEntity(this WeeklyRuleModel model, RuleData rule)
		{

		}
	}
}