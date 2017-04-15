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
using BookingPlatform.Backend.Entities;
using BookingPlatform.Constants;

namespace BookingPlatform.Models
{
	public class AdminBookingOverviewModel
	{
		public AdminBookingOverviewModel()
		{
			Bookings = new List<Booking>();
		}

		[Required(ErrorMessage = Strings.Admin.Calendar.InputErrorMonth)]
		[RegularExpression("^(1|2|3|4|5|6|7|8|9|10|11|12)$", ErrorMessage = Strings.Admin.Calendar.InputErrorMonth)]
		public int? Month { get; set; }

		[Required(ErrorMessage = Strings.Admin.Calendar.InputErrorYear)]
		[RegularExpression("^20(1|2|3|4|5|6|7|8|9)(0|1|2|3|4|5|6|7|8|9)$", ErrorMessage = Strings.Admin.Calendar.InputErrorYear)]
		public int? Year { get; set; }

		public DateTime FirstDayOfMonth { get; set; }
		public DateTime LastDayOfMonth { get; set; }

		public IList<Booking> Bookings { get; set; }

		public IEnumerable<SelectListItem> MonthList
		{
			get
			{
				foreach (var month in Enumerable.Range(1, 12))
				{
					yield return new SelectListItem
					{
						Text = DateTimeFormatInfo.CurrentInfo.GetMonthName(month),
						Value = month.ToString(),
						Selected = month == FirstDayOfMonth.Month
					};
				}
			}
		}

		public IEnumerable<SelectListItem> YearList
		{
			get
			{
				foreach (var year in Enumerable.Range(2010, 41))
				{
					yield return new SelectListItem
					{
						Text = year.ToString(),
						Value = year.ToString(),
						Selected = year == FirstDayOfMonth.Year
					};
				}
			}
		}
	}
}