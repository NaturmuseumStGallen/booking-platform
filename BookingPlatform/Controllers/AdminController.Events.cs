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

using System.Web.Mvc;
using BookingPlatform.Backend.DataAccess;
using BookingPlatform.Backend.Entities;
using BookingPlatform.Models;

namespace BookingPlatform.Controllers
{
	public partial class AdminController
	{
		[HttpGet]
		public ActionResult DeactivateEvent(int? id)
		{
			if (id.HasValue)
			{
				Database.Instance.DeactivateEvent(id.Value);
			}

			return RedirectToAction(nameof(EventOverview));
		}

		[HttpGet]
		public ActionResult EventOverview()
		{
			var model = new AdminEventOverviewModel();

			model.Events = Database.Instance.GetActiveEvents();

			return View(model);
		}

		[HttpGet]
		public ActionResult EventDetails(int? id)
		{
			var model = new AdminEventDetailsModel();

			if (id.HasValue && Database.Instance.IsValidEventId(id.Value))
			{
				var @event = Database.Instance.GetEventBy(id.Value);

				model.MapFromEntity(@event);
			}

			return View(model);
		}

		[HttpPost]
		public ActionResult EventDetails(AdminEventDetailsModel model)
		{
			if (ModelState.IsValid)
			{
				var @event = model.Id.HasValue ? Database.Instance.GetEventBy(model.Id.Value) : new Event();

				model.MapToEntity(@event);

				if (model.Id.HasValue)
				{
					Database.Instance.Update(@event);
				}
				else
				{
					Database.Instance.SaveNew(@event);
				}

				return RedirectToAction(nameof(EventOverview));
			}

			return View(model);
		}
	}
}