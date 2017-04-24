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
using BookingPlatform.Backend.Entities;
using BookingPlatform.Constants;

namespace BookingPlatform.Models
{
	public class AdminBookingDetailsModel : IValidatableObject
	{
		public AdminBookingDetailsModel()
		{
			Events = new List<Event>();
		}

		[MaxLength(100, ErrorMessage = Strings.Admin.BookingDetails.InputErrorMaxLength100)]
		public string Address { get; set; }

		[Required(ErrorMessage = Strings.Admin.BookingDetails.InputErrorCanton)]
		[MaxLength(100, ErrorMessage = Strings.Admin.BookingDetails.InputErrorMaxLength100)]
		public string Canton { get; set; }

		[Required(ErrorMessage = Strings.Admin.BookingDetails.InputErrorDate)]
		[RegularExpression("^((0[1-9])|([1-2][0-9])|(3[0-1])).((0[1-9])|(1[0-2])).20[1-5][0-9]$", ErrorMessage = Strings.Admin.BookingDetails.InputErrorDate)]
		public string Date { get; set; }

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

		[MaxLength(5000, ErrorMessage = Strings.Admin.BookingDetails.InputErrorMaxLength5000)]
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

		[Required(ErrorMessage = Strings.Admin.BookingDetails.InputErrorTime)]
		[RegularExpression("^((0[0-9])|(1[0-9])|(2[0-3])):((0[0-9])|([1-5][0-9]))$", ErrorMessage = Strings.Admin.BookingDetails.InputErrorTime)]
		public string Time { get; set; }

		[Required(ErrorMessage = Strings.Admin.BookingDetails.InputErrorTown)]
		[MaxLength(100, ErrorMessage = Strings.Admin.BookingDetails.InputErrorMaxLength100)]
		public string Town { get; set; }
		
		[RegularExpression("([1-9][0-9]{3})", ErrorMessage = Strings.Admin.BookingDetails.InputErrorZipCode)]
		public int? ZipCode { get; set; }

		public int? Id { get; set; }
		public bool IsActive { get; set; }
		public IList<Event> Events { get; set; }

		public bool IsNew
		{
			get { return !Id.HasValue; }
		}

		public IEnumerable<SelectListItem> EventListItems
		{
			get
			{
				yield return new SelectListItem { Text = Strings.Admin.BookingDetails.PleaseSelect, Value = "-1" };

				foreach (var @event in Events)
				{
					yield return new SelectListItem { Text = @event.Name, Value = @event.Id.ToString(), Selected = EventId == @event.Id };
				}
			}
		}

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			DateTime date;
			TimeSpan time;
			var results = new List<ValidationResult>();

			if (!DateTime.TryParse(Date, out date))
			{
				results.Add(new ValidationResult(Strings.Admin.BookingDetails.InputErrorDate, new[] { nameof(Date) }));
			}

			if (!TimeSpan.TryParse(Time, out time))
			{
				results.Add(new ValidationResult(Strings.Admin.BookingDetails.InputErrorTime, new[] { nameof(Time) }));
			}

			if (!results.Any())
			{
				results.Add(ValidationResult.Success);
			}

			return results;
		}
	}
}