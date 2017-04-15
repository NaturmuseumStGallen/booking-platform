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

using System.ComponentModel.DataAnnotations;
using BookingPlatform.Constants;

namespace BookingPlatform.Models
{
	public class AdminEventDetailsModel
	{
		[Required(ErrorMessage = Strings.Admin.EventDetails.InputErrorName)]
		[MaxLength(100, ErrorMessage = Strings.Admin.EventDetails.InputErrorMaxLength100)]
		public string Name { get; set; }

		[Required(ErrorMessage = Strings.Admin.EventDetails.InputErrorBlue)]
		[Range(0, 255, ErrorMessage = Strings.Admin.EventDetails.InputErrorBlue)]
		public int? Blue { get; set; }

		[Required(ErrorMessage = Strings.Admin.EventDetails.InputErrorGreen)]
		[Range(0, 255, ErrorMessage = Strings.Admin.EventDetails.InputErrorGreen)]
		public int? Green { get; set; }

		[Required(ErrorMessage = Strings.Admin.EventDetails.InputErrorRed)]
		[Range(0, 255, ErrorMessage = Strings.Admin.EventDetails.InputErrorRed)]
		public int? Red { get; set; }

		public int? Id { get; set; }

		public bool IsNew
		{
			get { return !Id.HasValue; }
		}
	}
}