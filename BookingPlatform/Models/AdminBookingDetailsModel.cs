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
using System.Web.Mvc;
using BookingPlatform.Constants;

namespace BookingPlatform.Models
{
	public class AdminBookingDetailsModel
	{
		public AdminBookingDetailsModel()
		{
			EventList = new List<SelectListItem>();
		}

		[MaxLength(100, ErrorMessage = Strings.Admin.BookingDetails.InputErrorMaxLength100)]
		public string Address { get; set; }

		[Required(ErrorMessage = Strings.Admin.BookingDetails.InputErrorDate)]
		public DateTime? Date { get; set; }

		[Required(ErrorMessage = Strings.Admin.BookingDetails.InputErrorEvent)]
		[Range(0, int.MaxValue, ErrorMessage = Strings.Admin.BookingDetails.InputErrorEvent)]
		public int? EventId { get; set; }

		[Required(ErrorMessage = Strings.Admin.BookingDetails.InputErrorEmail)]
		[EmailAddress(ErrorMessage = Strings.Admin.BookingDetails.InputErrorEmail)]
		[MaxLength(100, ErrorMessage = Strings.Admin.BookingDetails.InputErrorMaxLength100)]
		public string Email { get; set; }

		[Required(ErrorMessage = Strings.Admin.BookingDetails.InputErrorFirstName)]
		[MaxLength(100, ErrorMessage = Strings.Admin.BookingDetails.InputErrorMaxLength100)]
		public string FirstName { get; set; }

		[Required(ErrorMessage = Strings.Admin.BookingDetails.InputErrorGrade)]
		[MaxLength(100, ErrorMessage = Strings.Admin.BookingDetails.InputErrorMaxLength100)]
		public string Grade { get; set; }

		[Required(ErrorMessage = Strings.Admin.BookingDetails.InputErrorLastName)]
		[MaxLength(100, ErrorMessage = Strings.Admin.BookingDetails.InputErrorMaxLength100)]
		public string LastName { get; set; }

		[MaxLength(10000, ErrorMessage = Strings.Admin.BookingDetails.InputErrorMaxLength10000)]
		public string Notes { get; set; }

		[Required(ErrorMessage = Strings.Admin.BookingDetails.InputErrorNumberOfKids)]
		public int? NumberOfKids { get; set; }

		[Required(ErrorMessage = Strings.Admin.BookingDetails.InputErrorPhone)]
		[Phone(ErrorMessage = Strings.Admin.BookingDetails.InputErrorPhone)]
		[MaxLength(100, ErrorMessage = Strings.Admin.BookingDetails.InputErrorMaxLength100)]
		public string Phone { get; set; }

		[Required(ErrorMessage = Strings.Admin.BookingDetails.InputErrorSchool)]
		[MaxLength(100, ErrorMessage = Strings.Admin.BookingDetails.InputErrorMaxLength100)]
		public string School { get; set; }

		[Required(ErrorMessage = Strings.Admin.BookingDetails.InputErrorTown)]
		[MaxLength(100, ErrorMessage = Strings.Admin.BookingDetails.InputErrorMaxLength100)]
		public string Town { get; set; }

		[RegularExpression("([1-9][0-9]{3})", ErrorMessage = Strings.Admin.BookingDetails.InputErrorZipCode)]
		public int? ZipCode { get; set; }

		public int? Id { get; set; }
		public bool IsActive { get; set; }
		public bool IsNew { get; set; }

		public IList<SelectListItem> EventList { get; set; }
	}
}