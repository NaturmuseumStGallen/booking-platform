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

namespace BookingPlatform.Backend.Constants
{
	/// <summary>
	/// String ressource for the backend.
	/// </summary>
	public static class I18n
	{
		/// <summary>
		/// Strings used in the system mail sent when a new booking has been registered.
		/// </summary>
		public static class SystemEmail
		{
			public const string Address = "Adresse";
			public const string BookingDate = "Buchungsdatum";
			public const string Canton = "Kanton";
			public const string Email = "E-Mail-Adresse";
			public const string Event = "Führung";
			public const string FirstName = "Vorname";
			public const string Grade = "Klasse";
			public const string LastName = "Nachname";
			public const string NewBookingParagraph = "Eine neue Buchung ist übermittelt worden. Folgende Daten sind eingegeben worden:";
			public const string Notes = "Bemerkungen";
			public const string NumberOfKids = "Anzahl Kinder";
			public const string Phone = "Telefon";
			public const string School = "Schule";
			public const string Subject = "Neue Buchung";
			public const string Town = "Ort";
			public const string ZipCode = "PLZ";
		}
	}
}
