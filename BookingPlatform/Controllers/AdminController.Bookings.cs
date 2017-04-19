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
	public partial class AdminController
	{
		[HttpGet]
		public ActionResult BookingOverview(DateTime? date)
		{
			var model = new AdminBookingOverviewModel();

			date = date ?? DateTime.Today;

			model.FirstDayOfMonth = DateTimeUtility.GetFirstDayOfMonth(date.Value);
			model.LastDayOfMonth = DateTimeUtility.GetLastDayOfMonth(date.Value);
			model.Bookings = Database.Instance.GetBookings(model.FirstDayOfMonth, model.LastDayOfMonth);

			return View(model);
		}

		[HttpPost]
		public ActionResult BookingOverview(AdminBookingOverviewModel model)
		{
			if (ModelState.IsValid)
			{
				var date = new DateTime(model.Year.Value, model.Month.Value, 1);

				return RedirectToAction("BookingOverview", new { date });
			}

			return RedirectToAction("BookingOverview");
		}

		[HttpGet]
		public ActionResult BookingDetails(int? id)
		{
			var model = new AdminBookingDetailsModel();

			if (id.HasValue && Database.Instance.IsValidBookingId(id.Value))
			{
				var booking = Database.Instance.GetBookingBy(id.Value);

				model.MapFromEntity(booking);
			}

			model.Events = Database.Instance.GetActiveEvents();

			return View(model);
		}

		[HttpPost]
		public ActionResult BookingDetails(AdminBookingDetailsModel model)
		{
			if (ModelState.IsValid)
			{
				var booking = model.Id.HasValue ? Database.Instance.GetBookingBy(model.Id.Value) : new Booking();

				model.MapToEntity(booking);

				if (model.Id.HasValue)
				{
					Database.Instance.Update(booking);
				}
				else
				{
					Database.Instance.SaveNew(booking);
				}

				return RedirectToAction("BookingOverview", new { model.Date });
			}

			model.Events = Database.Instance.GetActiveEvents();

			return View(model);
		}

		[HttpGet]
		public ActionResult UpdateBookingState(int? id, bool? isActive)
		{
			if (id.HasValue && isActive.HasValue)
			{
				Database.Instance.UpdateBookingState(id.Value, !isActive.Value);

				return RedirectToAction("BookingDetails", new { id });
			}

			return RedirectToAction("BookingOverview");
		}
	}
}