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
using System.Web.Mvc;
using BookingPlatform.Backend.Scheduling;
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
			model.CalendarModel.Availability = new Scheduler();
			model.CalendarModel.CurrentDateTicks = DateTime.Today.Ticks;

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

			model.EventList = new List<SelectListItem>();
			model.CalendarModel = new BookingCalendarModel();
			model.CalendarModel.Days = new List<DateTime>();
			model.CalendarModel.Times = new List<DateTime>();

			return View(model);
		}

		[HttpGet]
		public ActionResult UpdateCalendar(int? eventId, long? ticks, BookingCalendarModel.Navigation? navigation)
		{
			if (eventId.HasValue && ticks.HasValue)
			{
				var model = new BookingCalendarModel();
				var current = new DateTime(ticks.Value);

				model.Days = new List<DateTime>();
				model.Times = new List<DateTime>();
				model.Availability = new Scheduler();

				model.CanNavigateToPreviousWeek = true;
				model.CanNavigateToPreviousMonth = true;

				if (navigation.HasValue && Enum.IsDefined(typeof(BookingCalendarModel.Navigation), navigation))
				{
					if (navigation == BookingCalendarModel.Navigation.PreviousMonth)
					{
						current = current.AddMonths(-1);
					}
					else if (navigation == BookingCalendarModel.Navigation.PreviousWeek)
					{
						current = current.AddDays(-7);
					}
					else if (navigation == BookingCalendarModel.Navigation.NextWeek)
					{
						current = current.AddDays(7);
					}
					else if (navigation == BookingCalendarModel.Navigation.NextMonth)
					{
						current = current.AddMonths(1);
					}
				}

				model.CurrentDateTicks = current.Ticks;
				model.Days.Add(current);
				model.Days.Add(current.AddDays(1));
				model.Days.Add(current.AddDays(2));
				model.Days.Add(current.AddDays(3));
				model.Days.Add(current.AddDays(4));
				model.Days.Add(current.AddDays(5));
				model.Days.Add(current.AddDays(6));

				model.Times.Add(new DateTime(1, 1, 1, 9, 0, 0));
				model.Times.Add(new DateTime(1, 1, 1, 10, 30, 0));
				model.Times.Add(new DateTime(1, 1, 1, 11, 0, 0));
				model.Times.Add(new DateTime(1, 1, 1, 13, 30, 0));

				return PartialView("_Calendar", model);
			}

			return new HttpNotFoundResult();
		}
	}
}