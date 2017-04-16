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
using System.Web.Mvc;
using BookingPlatform.Backend.Constants;
using BookingPlatform.Backend.Entities;
using BookingPlatform.Constants;

namespace BookingPlatform.Models
{
	public class AdminSettingsModel
	{
		public AdminSettingsModel()
		{
			Recipients = new List<EmailRecipient>();
			Rules = new List<RuleData>();
			Times = new List<TimeData>();
		}

		public string EmailTitle { get; set; }
		public string Password { get; set; }
		public string PlaintextContent { get; set; }
		public string HtmlContent { get; set; }

		public IList<EmailRecipient> Recipients { get; set; }
		public IList<RuleData> Rules { get; set; }
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

		public MvcHtmlString GetRuleDetails(RuleData rule)
		{
			return new MvcHtmlString("Rule details go here<br /> - Some property value <br /> - Yet another value");
		}
	}
}