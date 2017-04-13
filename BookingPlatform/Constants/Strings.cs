﻿/*
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

namespace BookingPlatform.Constants
{
	/// <summary>
	/// String ressource for the whole application, grouped by area and page.
	/// </summary>
	public static class Strings
	{
		/// <summary>
		/// Strings used on the admin pages.
		/// </summary>
		public static class Admin
		{
			public const string ActionsTitle = "Aktionen";
			public const string BookingPlatform = "Buchungsplattform";
			public const string Delete = "Löschen";
			public const string Refresh = "Aktualisieren";
			public const string SafetyMessage = "Sicher? Aktion kann nicht rückgängig gemacht werden...";
			public const string Save = "Speichern";

			public static class BookingDetails
			{
				public const string Activate = "Aktivieren";
				public const string Active = "Aktiv";
				public const string AdditionalInformation = "Zusätzliche Informationen";
				public const string Cancel = "Stornieren";
				public const string Cancelled = "Storniert";
				public const string ContactInformation = "Kontaktinformationen";
				public const string Event = "Führung";
				public const string InputErrorDate = "Bitte gültiges Datum eingeben (dd.mm.yyyy)!";
				public const string InputErrorEmail = "Bitte gültige E-Mail-Adresse eingeben!";
				public const string InputErrorEvent = "Bitte Führung auswählen!";
				public const string InputErrorGrade = "Bitte Klassenbezeichnung eingeben!";
				public const string InputErrorFirstName = "Bitte gültigen Vornamen eingeben!";
				public const string InputErrorLastName = "Bitte gültigen Nachnamen eingeben!";
				public const string InputErrorMaxLength100 = "Bitte maximal 100 Zeichen eingeben!";
				public const string InputErrorMaxLength10000 = "Bitte maximal 10'000 Zeichen eingeben!";
				public const string InputErrorNumberOfKids = "Bitte gültige Anzahl Kinder eingeben!";
				public const string InputErrorPhone = "Bitte gültige Telefonnummer eingeben!";
				public const string InputErrorSchool = "Bitte Name der Schule eingeben!";
				public const string InputErrorTime = "Bitte gültige Zeit eingeben (hh:mm)!";
				public const string InputErrorTown = "Bitte Wohnort eingeben!";
				public const string InputErrorZipCode = "Bitte gültige Postleitzahl eingeben!";
				public const string InputLabelAddress = "Adresse";
				public const string InputLabelDate = "Datum*";
				public const string InputLabelEmail = "E-Mail*";
				public const string InputLabelFirstName = "Vorname*";
				public const string InputLabelGrade = "Klassenbezeichnung*";
				public const string InputLabelLastName = "Nachname*";
				public const string InputLabelNotes = "Bemerkungen";
				public const string InputLabelNumberOfKids = "Anzahl Kinder*";
				public const string InputLabelPhone = "Telefon*";
				public const string InputLabelSchool = "Schule*";
				public const string InputLabelTime = "Zeit*";
				public const string InputLabelTown = "Ortschaft*";
				public const string InputLabelZipCode = "Postleitzahl";
				public const string PageTitleEdit = "Buchung bearbeiten";
				public const string PageTitleNew = "Neue Buchung erfassen";
				public const string PersonalInformation = "Persönliche Informationen";
				public const string PleaseSelect = " - Bitte wählen - ";
				public const string State = "Status";
			}

			public static class BookingOverview
			{
				public const string EditBooking = "Details";
				public const string NewBooking = "Neue Buchung erfassen";
				public const string Overview = "Übersicht";
				public const string PageTitle = "Buchungen";
				public const string TableHeadingDate = "Datum";
				public const string TableHeadingEvent = "Führung";
				public const string TableHeadingName = "Nachname, Vorname";
				public const string TableHeadingSchool = "Schule";
				public const string TableHeadingTown = "Ortschaft";
				public const string TableHeadingCancel = "Stornieren";
			}

			public static class Calendar
			{
				public const string PageTitle = "Monatsansicht";
				public const string InputErrorMonth = "Bitte gültigen Monat wählen!";
				public const string InputErrorYear = "Bitte gültiges Jahr eingeben!";
			}

			public static class EventDetails
			{
				public const string Color = "Farbe";
				public const string Deactivate = "Deaktivieren";
				public const string Example = "Beispiel";
				public const string InputErrorBlue = "Bitte gültigen Blau-Wert auswählen!";
				public const string InputErrorGreen = "Bitte gültigen Grün-Wert auswählen!";
				public const string InputErrorMaxLength100 = "Bitte maximal 100 Zeichen eingeben!";
				public const string InputErrorName = "Bitte gültigen Namen eingeben!";
				public const string InputErrorRed = "Bitte gültigen Rot-Wert auswählen!";
				public const string InputLabelBlue = "Blau-Wert";
				public const string InputLabelGreen = "Grün-Wert";
				public const string InputLabelName = "Name";
				public const string InputLabelRed = "Rot-Wert";
				public const string PageTitleNew = "Neue Führung erfassen";
				public const string PageTitleEdit = "Führung bearbeiten";
				public const string Preview = "Vorschau";
			}

			public static class EventGroupDetails
			{
				public const string InputErrorEvents = "Bitte mindestens 1 Führung auswählen!";
				public const string InputErrorMaxLength100 = "Bitte maximal 100 Zeichen eingeben!";
				public const string InputErrorName = "Bitte gültigen Namen eingeben!";
				public const string InputLabelEvents = "Führungen";
				public const string InputLabelName = "Name";
				public const string PageTitleNew = "Neue Führungsgruppe erfassen";
				public const string PageTitleEdit = "Führungsgruppe bearbeiten";
			}

			public static class EventOverview
			{
				public const string Edit = "Bearbeiten";
				public const string Events = "Führungen";
				public const string EventGroups = "Führungsgruppen";
				public const string Id = "ID";
				public const string Name = "Name";
				public const string NewEvent = "Neue Führung erfassen";
				public const string NewEventGroup = "Neue Führungsgruppe erfassen";
				public const string PageTitle = "Führungen";
			}

			public static class Overview
			{
				public const string PageTitle = "Übersicht";
				public const string SystemStatus = "System-Status";
			}

			public static class Settings
			{
				public const string Address = "Adresse";
				public const string AddNewRecipient = "EmpfängerIn hinzufügen:";
				public const string Content = "Inhalt Bestätigungsmail";
				public const string EMailConfiguration = "E-Mail Konfiguration";
				public const string Html = "HTML:";
				public const string PageTitle = "Einstellungen";
				public const string PlaceholderInfo = "Platzhalter: %%NAME%%, %%DATE%%, %%EVENT%%";
				public const string PlainText = "Nur-Text:";
				public const string Recipients = "EmpfängerInnen von System-Meldungen";
				public const string RuleOverview = "Übersicht Buchungsregeln";
			}
		}

		/// <summary>
		/// Strings used on the public pages.
		/// </summary>
		public static class Public
		{
			public const string AdditionalInformation = "Zusätzliche Informationen";
			public const string AvailabilityBooked = "Ausgebucht";
			public const string AvailabilityNotBookable = "Nicht buchbar";
			public const string ContactInformation = "Kontaktinformationen";
			public const string Date = "Datum";
			public const string Event = "Führung";
			public const string InputDescriptionGrade = "z.B. \"Mittelstufe, 5. + 6. Klasse\"";
			public const string InputDescriptionNumberOfKids = "min. 5 bis max. 30";
			public const string InputErrorDate = "Bitte wählen Sie einen Termin!";
			public const string InputErrorEmail = "Bitte geben Sie eine gültige E-Mail-Adresse an!";
			public const string InputErrorEvent = "Bitte wählen Sie eine Führung!";
			public const string InputErrorGrade = "Bitte geben Sie eine Klassenbezeichnung an!";
			public const string InputErrorFirstName = "Bitte geben Sie einen Vornamen ein!";
			public const string InputErrorLastName = "Bitte geben Sie einen Nachnamen ein!";
			public const string InputErrorMaxLength100 = "Bitte geben Sie maximal 100 Zeichen ein!";
			public const string InputErrorMaxLength10000 = "Bitte geben Sie maximal 10'000 Zeichen ein!";
			public const string InputErrorNumberOfKids = "Bitte geben Sie eine gültige Anzahl Kinder an (5 - 30)!";
			public const string InputErrorPhone = "Bitte geben Sie eine gültige Telefonnummer an!";
			public const string InputErrorSchool = "Bitte geben Sie den Namen Ihrer Schule an!";
			public const string InputErrorTown = "Bitte geben Sie einen Wohnort an!";
			public const string InputErrorZipCode = "Bitte geben Sie eine gültige Postleitzahl an!";
			public const string InputLabelAddress = "Adresse";
			public const string InputLabelEmail = "E-Mail*";
			public const string InputLabelFirstName = "Vorname*";
			public const string InputLabelGrade = "Klassenbezeichnung*";
			public const string InputLabelLastName = "Nachname*";
			public const string InputLabelNotes = "Bemerkungen";
			public const string InputLabelNumberOfKids = "Anzahl Kinder*";
			public const string InputLabelPhone = "Telefon*";
			public const string InputLabelSchool = "Schule*";
			public const string InputLabelTown = "Ortschaft*";
			public const string InputLabelZipCode = "Postleitzahl";
			public const string NavigationPreviousMonth = "«« Einen Monat zurück";
			public const string NavigationPreviousWeek = "« Eine Woche zurück";
			public const string NavigationNextWeek = "Eine Woche vor »";
			public const string NavigationNextMonth = "Einen Monat vor »»";
			public const string PageTitle = "Online-Buchung";
			public const string PersonalInformation = "Persönliche Informationen";
			public const string PleaseSelect = " - Bitte wählen - ";
			public const string PleaseSelectEvent = "Bitte wählen Sie eine Führung aus!";
			public const string Submit = "Abschicken";
		}
	}
}