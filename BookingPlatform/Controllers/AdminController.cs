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
using System.Web.Mvc;
using BookingPlatform.Backend.DataAccess;
using BookingPlatform.Backend.Scheduling;
using BookingPlatform.Models;

namespace BookingPlatform.Controllers
{
	public class AdminController : Controller
	{
		private const string SETTINGS_SAVED = "settings_saved";

		[HttpGet]
		public ActionResult Overview()
		{
			return View();
		}

		[HttpGet]
		public ActionResult Calendar(DateTime? date)
		{
			var model = new AdminCalendarModel();

			date = date ?? DateTime.Today;

			model.FirstDayOfMonth = DateTimeUtility.GetFirstDayOfMonth(date.Value);
			model.LastDayOfMonth = DateTimeUtility.GetLastDayOfMonth(date.Value);
			model.Bookings = Database.Instance.GetBookings(model.FirstDayOfMonth, model.LastDayOfMonth);

			return View(model);
		}

		[HttpPost]
		public ActionResult Calendar(AdminCalendarModel model)
		{
			if (ModelState.IsValid)
			{
				var date = new DateTime(model.Year.Value, model.Month.Value, 1);

				return RedirectToAction("Calendar", new { date });
			}

			return RedirectToAction("Calendar");
		}

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

			if (!id.HasValue)
			{
				model.IsNew = true;
			}

			model.Events = Database.Instance.GetActiveEvents();

			return View(model);
		}

		[HttpPost]
		public ActionResult BookingDetails(AdminBookingDetailsModel model)
		{
			if (ModelState.IsValid)
			{
				// TODO: Save or update booking!

				return RedirectToAction("BookingOverview", new { model.Date });
			}

			model.Events = Database.Instance.GetActiveEvents();

			return View(model);
		}

		[HttpGet]
		public ActionResult EventOverview()
		{
			var model = new AdminEventOverviewModel();

			model.Events = Database.Instance.GetActiveEvents();
			model.EventGroups = Database.Instance.GetEventGroups();

			return View(model);
		}
		
		[HttpGet]
		public ActionResult EventDetails(int? id)
		{
			var model = new AdminEventDetailsModel();

			if (!id.HasValue)
			{
				model.IsNew = true;
			}

			return View(model);
		}

		[HttpPost]
		public ActionResult EventDetails(AdminEventDetailsModel model)
		{
			if (ModelState.IsValid)
			{
				return RedirectToAction("EventOverview");
			}

			return View(model);
		}

		[HttpGet]
		public ActionResult EventGroupDetails(int? id)
		{
			var model = new AdminEventGroupDetailsModel();

			if (!id.HasValue)
			{
				model.IsNew = true;
			}

			model.AvailableEvents = Database.Instance.GetActiveEvents();

			return View(model);
		}

		[HttpPost]
		public ActionResult EventGroupDetails(AdminEventGroupDetailsModel model)
		{
			if (ModelState.IsValid)
			{
				return RedirectToAction("EventOverview");
			}

			return View(model);
		}

		[HttpGet]
		public ActionResult Settings()
		{
			var model = new AdminSettingsModel();

			model.Recipients.Add(new AdminSettingsModel.RecipientModel { EMail = "blubb@sdf.ch", Id = 3 });
			model.Recipients.Add(new AdminSettingsModel.RecipientModel { EMail = "blubb@sdf.ch", Id = 3 });
			model.Recipients.Add(new AdminSettingsModel.RecipientModel { EMail = "blubb@sdf.ch", Id = 3 });
			model.Recipients.Add(new AdminSettingsModel.RecipientModel { EMail = "blubb@sdf.ch", Id = 3 });
			model.Recipients.Add(new AdminSettingsModel.RecipientModel { EMail = "blubb@sdf.ch", Id = 3 });

			return View(model);
		}

		[HttpPost]
		public ActionResult NewEmailRecipient(string email)
		{
			if (ModelState.IsValid)
			{
			}

			return RedirectToAction("Settings");
		}
	}
}