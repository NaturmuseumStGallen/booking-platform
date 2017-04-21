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

using System.Configuration;

namespace BookingPlatform.Backend.Configuration
{
	public static class AppConfig
	{
		public static string EmailSenderAddress
		{
			get { return ConfigurationManager.AppSettings["EmailSenderAddress"]; }
		}

		public static string DataSource
		{
			get { return ConfigurationManager.AppSettings["DataSource"]; }
		}

		public static string InitialCatalog
		{
			get { return ConfigurationManager.AppSettings["InitialCatalog"]; }
		}

		public static string Password
		{
			get { return ConfigurationManager.AppSettings["Password"]; }
		}

		public static string SendGridApiKey
		{
			get { return ConfigurationManager.AppSettings["SendGridApiKey"]; }
		}

		public static string UserId
		{
			get { return ConfigurationManager.AppSettings["UserId"]; }
		}
	}
}
