using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BookingPlatform.Backend.Booking;
using BookingPlatform.Models;

namespace BookingPlatform.Controllers
{
	public class BookingController : Controller
	{
		[HttpGet]
		public ActionResult Index(int? id)
		{
			var model = new BookingModel();

			model.EventList = new List<SelectListItem>
			{
				new SelectListItem { Text = "Bitte wählen", Value = "-1" },
				new SelectListItem { Text = "Führung A", Value = "1" },
				new SelectListItem { Text = "Führung B", Value = "2" },
				new SelectListItem { Text = "Führung C", Value = "3" },
				new SelectListItem { Text = "Führung D", Value = "4" }
			};

			model.CalendarModel = new BookingCalendarModel();
			model.CalendarModel.Days = new List<DateTime>();
			model.CalendarModel.Times = new List<DateTime>();
			model.CalendarModel.Availability = new AvailabilityProvider();

			model.CalendarModel.Days.Add(DateTime.Today);
			model.CalendarModel.Days.Add(DateTime.Today.AddDays(1));
			model.CalendarModel.Days.Add(DateTime.Today.AddDays(2));
			model.CalendarModel.Days.Add(DateTime.Today.AddDays(3));
			model.CalendarModel.Days.Add(DateTime.Today.AddDays(4));
			model.CalendarModel.Days.Add(DateTime.Today.AddDays(5));
			model.CalendarModel.Days.Add(DateTime.Today.AddDays(6));

			model.CalendarModel.Times.Add(new DateTime(1, 1, 1, 9, 0, 0));
			model.CalendarModel.Times.Add(new DateTime(1, 1, 1, 10, 30, 0));
			model.CalendarModel.Times.Add(new DateTime(1, 1, 1, 11, 0, 0));
			model.CalendarModel.Times.Add(new DateTime(1, 1, 1, 13, 30, 0));

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index(BookingModel model)
		{
			if (ModelState.IsValid)
			{
				return Content("Success!");
			}

			return View(model);
		}
	}
}