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
using System.ComponentModel.DataAnnotations;

namespace BookingPlatform.Models
{
	public static class ValidationUtility
	{
		public static bool AreNotNullOrWhitespace(params string[] strings)
		{
			var valid = true;

			foreach (var @string in strings)
			{
				valid &= !String.IsNullOrWhiteSpace(@string);
			}

			return valid;
		}

		public static bool IsValidEmail(string email)
		{
			return new EmailAddressAttribute().IsValid(email);
		}

		public static bool IsValidPassword(string password)
		{
			return !String.IsNullOrWhiteSpace(password);
		}

		public static bool IsValidTime(string time)
		{
			TimeSpan timeSpan;

			return TimeSpan.TryParse(time, out timeSpan);
		}
	}
}