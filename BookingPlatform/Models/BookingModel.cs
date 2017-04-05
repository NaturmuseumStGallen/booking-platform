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

		public int? DateId { get; set; }

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
		[Range(1, 200, ErrorMessage = Strings.Public.InputErrorNumberOfKids)]
		[RegularExpression("([1-9][0-9]*)", ErrorMessage = Strings.Public.InputErrorNumberOfKids)]
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

		public int? ZipCode { get; set; }

		public IList<SelectListItem> EventList { get; set; }
	}
}