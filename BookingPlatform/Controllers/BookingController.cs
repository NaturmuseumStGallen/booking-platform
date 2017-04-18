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
using System.Web.Mvc;
using BookingPlatform.Backend.DataAccess;
using BookingPlatform.Backend.Entities;
using BookingPlatform.Models;
using BookingPlatform.Utilities;

namespace BookingPlatform.Controllers
{
	[RequireHttps]
	public class BookingController : Controller
	{
		[HttpGet]
		public ActionResult Form(int? id)
		{
			var model = new BookingModel();

			model.Events = Database.Instance.GetActiveEvents();
			model.CalendarModel.CurrentDateTicks = DateTime.Today.Ticks;
			model.CalendarModel.ShowEventSelectionMessage = !id.HasValue;

			if (id.HasValue && Database.Instance.IsValidEventId(id.Value))
			{
				model.CalendarModel.Dates = CalendarUtility.CalculateBookingDates(DateTime.Today, id.Value);
			}

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Form(BookingModel model)
		{
			if (ModelState.IsValid)
			{
				var booking = new Booking();

				model.MapToEntity(booking);

				Database.Instance.SaveNew(booking);

				// TODO:
				// - Send email
				// - Return success page with most important booking information & note about email confirmation
				return Content("Success!");
			}

			model.Events = Database.Instance.GetActiveEvents();
			model.CalendarModel.CurrentDateTicks = DateTime.Today.Ticks;
			model.CalendarModel.ShowEventSelectionMessage = !model.EventId.HasValue;

			if (model.EventId.HasValue && Database.Instance.IsValidEventId(model.EventId.Value))
			{
				model.CalendarModel.Dates = CalendarUtility.CalculateBookingDates(DateTime.Today, model.EventId.Value);
			}

			return View(model);
		}

		[HttpGet]
		public ActionResult UpdateCalendar(int? eventId, long? ticks, CalendarUtility.Navigation? navigation)
		{
			if (!ticks.HasValue)
			{
				return new HttpNotFoundResult();
			}

			var model = new BookingCalendarModel();
			var current = CalendarUtility.CalculateNewDate(new DateTime(ticks.Value), navigation);

			model.CurrentDateTicks = current.Ticks;

			if (eventId.HasValue && Database.Instance.IsValidEventId(eventId.Value))
			{
				model.Dates = CalendarUtility.CalculateBookingDates(current, eventId.Value);
				model.CanNavigateToPreviousWeek = CalendarUtility.CanNavigateToPreviousWeek(current);
				model.CanNavigateToPreviousMonth = CalendarUtility.CanNavigateToPreviousMonth(current);
			}
			else
			{
				model.ShowEventSelectionMessage = true;
			}

			return PartialView("_Calendar", model);
		}
	}
}