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

using System.Threading;
using System.Web.Mvc;
using BookingPlatform.Backend.DataAccess;
using BookingPlatform.Models;
using BookingPlatform.Utilities;

namespace BookingPlatform.Controllers
{
	[RequireHttps]
	public partial class AdminController : Controller
	{
		[HttpGet]
		public ActionResult Overview()
		{
			var model = new AdminOverviewModel();

			model.Initialize();

			return View(model);
		}

		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(string password)
		{
			if (Authenticator.IsAuthenticated() || Authenticator.TryToAuthenticate(password))
			{
				return RedirectToAction("Overview");
			}

			// A short delay to make brute force attacks less effective...
			Thread.Sleep(2000);

			return View();
		}

		[HttpGet]
		public ActionResult Logout()
		{
			Authenticator.Logout();

			return RedirectToAction("Login");
		}

		protected override void OnActionExecuting(ActionExecutingContext context)
		{
			if (context.ActionDescriptor.ActionName != nameof(Login) && !Authenticator.IsAuthenticated())
			{
				context.Result = RedirectToAction("Login");

				return;
			}

			base.OnActionExecuting(context);
		}
	}
}