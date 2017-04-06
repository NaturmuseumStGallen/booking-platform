/*
 * Copyright (C) 2017 Naturmuseum St. Gallen
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
	public class BookingModel
	{
		[MaxLength(100, ErrorMessage = Strings.Public.InputErrorMaxLength100)]
		public string Address { get; set; }

		[Required(ErrorMessage = Strings.Public.InputErrorDate)]
		public DateTime? Date { get; set; }

		[Required(ErrorMessage = Strings.Public.InputErrorEmail)]
		[EmailAddress(ErrorMessage = Strings.Public.InputErrorEmail)]
		[MaxLength(100, ErrorMessage = Strings.Public.InputErrorMaxLength100)]
		public string Email { get; set; }

		[Required(ErrorMessage = Strings.Public.InputErrorEvent)]
		[Range(0, int.MaxValue, ErrorMessage = Strings.Public.InputErrorEvent)]
		public int? EventId { get; set; }

		[Required(ErrorMessage = Strings.Public.InputErrorFirstName)]
		[MaxLength(100, ErrorMessage = Strings.Public.InputErrorMaxLength100)]
		public string FirstName { get; set; }

		[Required(ErrorMessage = Strings.Public.InputErrorGrade)]
		[MaxLength(100, ErrorMessage = Strings.Public.InputErrorMaxLength100)]
		public string Grade { get; set; }

		[Required(ErrorMessage = Strings.Public.InputErrorLastName)]
		[MaxLength(100, ErrorMessage = Strings.Public.InputErrorMaxLength100)]
		public string LastName { get; set; }

		[MaxLength(10000, ErrorMessage = Strings.Public.InputErrorMaxLength10000)]
		public string Notes { get; set; }

		[Required(ErrorMessage = Strings.Public.InputErrorNumberOfKids)]
		[Range(5, 30, ErrorMessage = Strings.Public.InputErrorNumberOfKids)]
		[RegularExpression("(5|6|7|8|9|[1-2][0-9]|30)", ErrorMessage = Strings.Public.InputErrorNumberOfKids)]
		public int? NumberOfKids { get; set; }

		[Required(ErrorMessage = Strings.Public.InputErrorPhone)]
		[Phone(ErrorMessage = Strings.Public.InputErrorPhone)]
		[MaxLength(100, ErrorMessage = Strings.Public.InputErrorMaxLength100)]
		public string Phone { get; set; }

		[Required(ErrorMessage = Strings.Public.InputErrorSchool)]
		[MaxLength(100, ErrorMessage = Strings.Public.InputErrorMaxLength100)]
		public string School { get; set; }

		[Required(ErrorMessage = Strings.Public.InputErrorTown)]
		[MaxLength(100, ErrorMessage = Strings.Public.InputErrorMaxLength100)]
		public string Town { get; set; }

		[RegularExpression("([1-9][0-9]{3})", ErrorMessage = Strings.Public.InputErrorZipCode)]
		public int? ZipCode { get; set; }

		public IList<SelectListItem> EventList { get; set; }

		public BookingCalendarModel CalendarModel { get; set; }
	}
}