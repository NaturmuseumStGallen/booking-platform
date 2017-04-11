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
using BookingPlatform.Backend.Scheduling;
using BookingPlatform.Models;

namespace BookingPlatform.Controllers
{
	public class AdminController : Controller
	{
		[HttpGet]
		public ActionResult Overview()
		{
			return View();
		}

		[HttpGet]
		public ActionResult Calendar()
		{
			var model = new AdminCalendarModel();

			model.FirstDayOfMonth = DateTimeUtility.GetFirstDayOfMonth(DateTime.Today);
			model.LastDayOfMonth = DateTimeUtility.GetLastDayOfMonth(DateTime.Today);
			model.Bookings = Database.Instance.GetBookings(model.FirstDayOfMonth, model.LastDayOfMonth);

			return View(model);
		}

		[HttpPost]
		public ActionResult Calendar(AdminCalendarModel model)
		{
			var date = new DateTime(model.Year.Value, model.Month.Value, 1);

			model.FirstDayOfMonth = DateTimeUtility.GetFirstDayOfMonth(date);
			model.LastDayOfMonth = DateTimeUtility.GetLastDayOfMonth(date);
			model.Bookings = Database.Instance.GetBookings(model.FirstDayOfMonth, model.LastDayOfMonth);

			return View(model);
		}

		[HttpGet]
		public ActionResult BookingOverview()
		{
			return View();
		}

		[HttpGet]
		public ActionResult EventOverview()
		{
			return View();
		}

		[HttpGet]
		public ActionResult Settings()
		{
			return View();
		}
	}
}