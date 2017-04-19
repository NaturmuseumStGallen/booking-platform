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
using System.Web;
using BookingPlatform.Backend.DataAccess;
using BookingPlatform.Backend.Security;

namespace BookingPlatform.Utilities
{
	public static class Authenticator
	{
		private const string AUTHENTICATED = "Authenticated";

		public static bool IsAuthenticated()
		{
			return HttpContext.Current.Session[AUTHENTICATED] as bool? == true;
		}

		public static bool TryToAuthenticate(string password)
		{
			var hash = string.Empty;
			var settings = Database.Instance.GetSettings();

			try
			{
				hash = Password.ComputeHash(password, settings.PasswordSalt);
			}
			catch (Exception)
			{
				// Password validation failed
			}

			if (Password.AreEqual(settings.PasswordHash, hash))
			{
				HttpContext.Current.Session[AUTHENTICATED] = true;

				return true;
			}

			return false;
		}

		public static void Logout()
		{
			HttpContext.Current.Session[AUTHENTICATED] = false;
		}
	}
}