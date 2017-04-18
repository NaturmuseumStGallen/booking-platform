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
using BookingPlatform.Backend.Scheduling;
using BookingPlatform.Models;

namespace BookingPlatform.Controllers
{
	public class BookingController : Controller
	{
		[HttpGet]
		public ActionResult Form(int? id)
		{
			var model = new BookingModel();
			var scheduler = Scheduler.CreateNew(Database.Instance);
			var monday = DateTimeUtility.GetMondayOfWeekFor(DateTime.Today);
			var sunday = DateTimeUtility.GetSundayOfWeekFor(DateTime.Today);

			model.Events = Database.Instance.GetActiveEvents();
			model.CalendarModel.CurrentDateTicks = DateTime.Today.Ticks;

			if (id.HasValue && Database.Instance.IsValidEventId(id.Value))
			{
				var @event = Database.Instance.GetEventBy(id.Value);

				model.CalendarModel.Dates = scheduler.GetBookingDateRange(monday, sunday, @event);
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

			var scheduler = Scheduler.CreateNew(Database.Instance);
			var monday = DateTimeUtility.GetMondayOfWeekFor(DateTime.Today);
			var sunday = DateTimeUtility.GetSundayOfWeekFor(DateTime.Today);

			model.Events = Database.Instance.GetActiveEvents();
			model.CalendarModel.CurrentDateTicks = DateTime.Today.Ticks;

			if (model.EventId.HasValue && Database.Instance.IsValidEventId(model.EventId.Value))
			{
				var @event = Database.Instance.GetEventBy(model.EventId.Value);

				model.CalendarModel.Dates = scheduler.GetBookingDateRange(monday, sunday, @event);
			}

			return View(model);
		}

		[HttpGet]
		public ActionResult UpdateCalendar(int? eventId, long? ticks, BookingCalendarModel.Navigation? navigation)
		{
			if (!ticks.HasValue)
			{
				return new HttpNotFoundResult();
			}

			var model = new BookingCalendarModel();
			var current = new DateTime(ticks.Value);

			if (eventId.HasValue && Database.Instance.IsValidEventId(eventId.Value))
			{
				var scheduler = Scheduler.CreateNew(Database.Instance);
				var monday = DateTimeUtility.GetMondayOfWeekFor(current);
				var sunday = DateTimeUtility.GetSundayOfWeekFor(current);

				model.Dates = scheduler.GetBookingDateRange(monday, sunday, new Event());

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
			}
			else
			{
				model.ShowEventSelectionMessage = true;
			}

			model.CurrentDateTicks = current.Ticks;

			return PartialView("_Calendar", model);
		}
	}
}