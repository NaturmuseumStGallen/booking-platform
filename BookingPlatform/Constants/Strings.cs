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

using System;
using System.Collections.Generic;
using BookingPlatform.Backend.Constants;
using BookingPlatform.Backend.Emails;

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
            public const string Add = "Hinzufügen";
            public const string BookingPlatform = "Buchungsplattform";
			public const string Create = "Erstellen";
			public const string Delete = "Löschen";
			public const string Edit = "Bearbeiten";
			public const string Refresh = "Aktualisieren";
			public const string SafetyMessage = "Sicher? Aktion kann nicht rückgängig gemacht werden...";
			public const string Save = "Speichern";
            
            public static string GetRuleTypeName(RuleType type)
			{
				switch (type)
				{
					case RuleType.DateRange:
						return Settings.DateRangeRule;
					case RuleType.EventDuration:
						return Settings.EventDurationRule;
					case RuleType.EventGroup:
						return Settings.EventGroupRule;
					case RuleType.MinimumDate:
						return Settings.MinimumDateRule;
					case RuleType.Weekly:
						return Settings.WeeklyRule;
                    case RuleType.BookingTimeOverride:
                        return Settings.BookingTimeOverrideRule;
                    case RuleType.MultipleBooking:
                        return Settings.MultipleBookingRule;
                    default:
						throw new InvalidOperationException(String.Format("Rule of type '{0}' not yet configured!", type));
				}
			}

			public static string GetStatusName(AvailabilityStatus status)
			{
				switch (status)
				{
					case AvailabilityStatus.Booked:
						return RuleDetails.RuleStatusBooked;
					case AvailabilityStatus.Free:
						return RuleDetails.RuleStatusFree;
					case AvailabilityStatus.NotBookable:
						return RuleDetails.RuleStatusNotBookable;
					default:
						throw new InvalidOperationException(String.Format("Status of type '{0}' not yet configured!", status));
				}
			}

			public static class BookingDetails
			{
				public const string Activate = "Aktivieren";
				public const string Active = "Aktiv";
				public const string AdditionalInformation = "Zusätzliche Informationen";
				public const string Cancel = "Stornieren";
				public const string Cancelled = "Storniert";
				public const string ContactInformation = "Kontaktinformationen";
				public const string Event = "Führung";
				public const string InputErrorCanton = "Bitte gültigen Kanton eingeben!";
				public const string InputErrorDate = "Bitte gültiges Datum eingeben (dd.mm.yyyy)!";
				public const string InputErrorEmail = "Bitte gültige E-Mail-Adresse eingeben!";
				public const string InputErrorEvent = "Bitte Führung auswählen!";
				public const string InputErrorGrade = "Bitte Klassenbezeichnung eingeben!";
				public const string InputErrorFirstName = "Bitte gültigen Vornamen eingeben!";
				public const string InputErrorLastName = "Bitte gültigen Nachnamen eingeben!";
				public const string InputErrorMaxLength100 = "Bitte maximal 100 Zeichen eingeben!";
				public const string InputErrorMaxLength5000 = "Bitte maximal 5'000 Zeichen eingeben!";
				public const string InputErrorNumberOfKids = "Bitte gültige Anzahl Kinder eingeben!";
				public const string InputErrorPhone = "Bitte gültige Telefonnummer eingeben!";
				public const string InputErrorSchool = "Bitte Name der Schule eingeben!";
				public const string InputErrorTime = "Bitte gültige Zeit eingeben (hh:mm)!";
				public const string InputErrorTown = "Bitte Wohnort eingeben!";
				public const string InputErrorZipCode = "Bitte gültige Postleitzahl eingeben!";
				public const string InputLabelAddress = "Adresse";
				public const string InputLabelCanton = "Kanton*";
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
				public const string State = "Status:";
			}

			public static class BookingOverview
			{
				public const string Active = "Aktiv";
                public const string Cancelled = "Storniert";
                public const string Created = "Erstellt";
                public const string Day = "Tag";
                public const string EditBooking = "Details";
				public const string NewBooking = "Neue Buchung erfassen";
				public const string Overview = "Übersicht";
				public const string PageTitle = "Buchungen";
				public const string TableHeadingBookingState = "Status";
				public const string TableHeadingDate = "Datum";
				public const string TableHeadingEvent = "Führung";
				public const string TableHeadingName = "Nachname, Vorname";
				public const string TableHeadingSchool = "Schule";
				public const string TableHeadingTown = "Ortschaft";
                public const string Time = "Zeit";
                public const string Unknown = "unbekannt";
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
				public const string PageTitleEdit = "Führung bearbeiten";
				public const string PageTitleNew = "Neue Führung erfassen";
				public const string Preview = "Vorschau";
			}

			public static class EventOverview
			{
				public const string Events = "Führungen";
				public const string Id = "ID";
				public const string Name = "Name";
				public const string NewEvent = "Neue Führung erfassen";
                public const string DisplayOrder = "Anzeige Reihenfolge speichern";
                public const string PageTitle = "Führungen";
			}

			public static class Login
			{
				public const string InputLabelPassword = "Passwort";
				public const string LoginText = "Für den Zugriff auf den Administrationsbereich müssen Sie sich authentifizieren.";
				public const string LogoutText = "Abmelden";
				public const string PageTitle = "Login";
				public const string SectionTitle = "Administrationsbereich";
				public const string Submit = "Einloggen";
				public const string WrongPassword = "Falsches Passwort!";
			}

			public static class Overview
			{
				public const string ActiveEventsCount = "Anzahl aktiver Führungen:";
				public const string CurrentData = "Aktuelle Daten";
				public const string NewestBooking = "Neueste Buchung:";
				public const string NoWarnings = "Keine Meldungen oder Warnungen verfügbar.";
				public const string PageTitle = "Übersicht";
				public const string StatisticalInfos = "Statistische Informationen";
				public const string PendingBookingsCount = "Anzahl ausstehender, aktiver Buchungen:";
				public const string RulesCount = "Anzahl konfigurierter Buchungsregeln:";
				public const string SystemMessages = "System-Meldungen";
				public const string SystemStatus = "System-Status";
				public const string TotalBookingCount = "Gesamtzahl im System gespeicherter Buchungen:";
				public const string Warning = "WARNUNG";
				public const string WarningNoConfirmationContent = "Kein Inhalt für die Bestätigungsseite konfiguriert!";
				public const string WarningNoEmailContent = "Kein Inhalt für das Bestätigungsmail konfiguriert!";
				public const string WarningNoEmailRecipients = "Keine E-Mail-EmpfängerInnen konfiguriert! Systemnachrichten werden nicht versandt!";
				public const string WarningNoEvents = "Keine Führungen konfiguriert!";
				public const string WarningNoTimes = "Keine Buchungszeiten konfiguriert! Die BenutzerInnen können keine Buchung vornehmen!";
			}

			public static class RuleDetails
			{
				public const string Configuration = "Konfiguration";
				public const string Description = "Beschreibung";
				public const string InputErrorDate = "Bitte gültiges Datum eingeben (dd.mm.yyyy)!";
				public const string InputErrorDays = "Bitte gültige Anzahl Tage eingeben (0 oder mehr)!";
				public const string InputErrorDayOfWeek = "Bitte gültigen Wochentag auswählen!";
				public const string InputErrorEvent = "Bitte Führung auswählen!";
				public const string InputErrorEvents = "Bitte mindestens 2 Führungen auswählen!";
				public const string InputErrorInvalidDateRange = "Ungültiger Datumsbereich, Startdatum muss kleiner sein als Enddatum!";
				public const string InputErrorMaxLength100 = "Bitte maximal 100 Zeichen eingeben!";
				public const string InputErrorName = "Bitte gültigen Namen eingeben!";
				public const string InputErrorTime = "Bitte gültige Zeit eingeben (hh:mm)!";
				public const string InputErrorStatus = "Bitte gültigen Status auswählen!";
                public const string InputErrorOverrideTimes = "Bitte Zeiten auswählen !";
                public const string InputErrorNumberOfParallelBookings = "Bitte gültige Anzahl gleichzeitige Führungen eingeben!";
                public const string InputLabelDate = "Datum";
				public const string InputLabelDays = "Anzahl Tage";
				public const string InputLabelDayOfWeek = "Wochentag";
				public const string InputLabelEndDate = "Enddatum";
				public const string InputLabelEndTime = "Endzeit";
				public const string InputLabelEvent = "Führung";
				public const string InputLabelEvents = "Führungen";
				public const string InputLabelName = "Name";
				public const string InputLabelRuleScope = "Gültig für";
				public const string InputLabelStartDate = "Startdatum";
				public const string InputLabelStartTime = "Startzeit";
				public const string InputLabelStatus = "Buchungsstatus";
				public const string InputLabelTime = "Zeit";
                public const string InputLabelNumberOfParallelBookings = "Anzahl gleichzeitiger Führungen";
                public const string PageTitleEdit = "Regel bearbeiten:";
				public const string PageTitleNew = "Neue Regel erfassen:";
				public const string RuleStatusBooked = "Ausgebucht";
				public const string RuleStatusFree = "Frei";
				public const string RuleStatusNotBookable = "Nicht buchbar";
				public const string ScopeAllEvents = "Alle Führungen";

                public static class Descriptions
				{
					public const string Important = "WICHTIG:";

					public static string DateRangeRule = @"Erstellt eine Regel für ein einzelnes Datum oder eine Zeitperiode. Der Gültigkeitsbereich
						kann optional auf eine einzelne Führung beschränkt werden. Es müssen mindestens der Gültigkeitsbereich, ein Startdatum
						und ein Status gewählt werden. Folgende Eingabekombinationen sind möglich:";

					public static string EventDurationRule = @"Erstellt eine Regel, welche den gültigen Buchungszeitraum für eine Führung
						definiert. Das heisst, dass für die gewählte Führung alle Termine, die ausserhalb des gewählten Zeitraums liegen,
						als " + "\"" + RuleStatusNotBookable + "\" dargestellt werden.";

					public const string EventGroupRule = @"Erstellt eine Führungsgruppen-Regel. Eine Führungsgruppe kann eine oder mehrere
						Führungen beinhalten. Diese Regel bewirkt, dass zum gleichen Zeitpunkt nur eine Führung der Gruppe gebucht werden
						kann, dass also alle anderen Führungen derselben Gruppe als " + "\"" + RuleStatusBooked + "\" dargestellt werden.";

					public const string MinimumDateRule = @"Erstellt eine Regel für den ersten (frühsten) buchbaren Termin. Es sind zwei
						verschiedene Typen möglich:";

					public const string PriorityNote = "Die Status haben eine prioritäre Gewichtung: \"" + RuleStatusBooked + "\" ist stärker als \"" +
						RuleStatusFree + "\" ist stärker als \"" + RuleStatusNotBookable + "\". " + @"Sollten also zwei oder mehrere
						Regeln für einen bestimmten Buchungstermin ansprechen, wird der Status mit der höchsten Priorität angezeigt.";

					public const string WeeklyRule = @"Erstellt eine sich wöchentlich wiederholende Regel. Es müssen mindestens ein Wochentag
						und ein Status gewählt werden. Folgende Eingabekombinationen sind möglich:";

                    public const string BookingTimeOverrideRule = @"Erstellt eine Regel, welche die global definierten Buchungszeiten für die ausgewählte Führung übersteuert.";

                    public const string MultipleBookingRule = @"Erstellt eine Regel, welche festlegt wie viele Führungen parallel zu einem Zeitpunkt gebucht werden können.
                    Die Anzahl paralleler Führungen muss grösser als 1 sein.
                    Beispiel: Wenn bei 'Anzahl gleichzeitiger Führungen' 2 eingegeben wird, können zur gleichen Zeit am gleichen Tag 2 Führungen gebucht werden.";

                    public static readonly KeyValuePair<string, string>[] DateRangeOptions = new KeyValuePair<string, string>[]
					{
						new KeyValuePair<string, string>("Nur Startdatum:", "An dem angegebenen Datum wird für den ganzen Tag der gewählte Status angezeigt."),
						new KeyValuePair<string, string>("Startdatum und Startzeit:", "Für Führungen am angegebenen Datum zur angegebenen Zeit wird der gewählte Status angezeigt."),
						new KeyValuePair<string, string>("Startdatum, Startzeit und Endzeit:", "Für Führungen am angegebenen Datum innerhalb des definierten Zeitabschnitts wird der gewählte Status angezeigt."),
						new KeyValuePair<string, string>("Startdatum und Enddatum:", "Für den Zeitraum vom angegebenen Startdatum um 00:00 Uhr bis zum angegebenen Enddatum um 00:00 Uhr wird der gewählte Status angezeigt."),
						new KeyValuePair<string, string>("Startdatum, Startzeit und Enddatum:", "Gleich wie vorangehender Punkt, einfach mit angegebener Startzeit statt 00:00 Uhr."),
						new KeyValuePair<string, string>("Startdatum, Startzeit, Enddatum und Endzeit:", "Gleich wie vorangehender Punkt, nun aber mit Start- und Endzeit wie eingegeben.")
					};

					public static readonly KeyValuePair<string, string>[] MinimumDateOptions = new KeyValuePair<string, string>[]
					{
						new KeyValuePair<string, string>("Fixes Datum:", "Alle vor dem angegebenen Datum (z.B. 03.04.2017 10:00) liegenden Termine werden als \"" + RuleStatusNotBookable + "\" dargestellt."),
						new KeyValuePair<string, string>("Anzahl Tage:", "Definiert eine gesperrte Zeitperiode ab jeweils dem aktuellen Datum eines Tages, z.B. heute + 5 Tage.")
					};

					public static readonly KeyValuePair<string, string>[] WeeklyOptions = new KeyValuePair<string, string>[]
					{
                        new KeyValuePair<string, string>("Nur Wochentag:", "An dem angegebenen Wochentag wird für den ganzen Tag der gewählte Status angezeigt."),
						new KeyValuePair<string, string>("Wochentag und Zeit:", "An dem angegebenen Wochentag wird zur eingegebenen Zeit der gewählte Status angezeigt."),
                        new KeyValuePair<string, string>("Wochentag, Zeit und Endzeit:", "An dem angegebenen Wochentag wird zur eingegebenen Zeit der gewählte Status bis zur Endzeit angezeigt."),
                        new KeyValuePair<string, string>("Wochentag und Endzeit:", "An dem angegebenen Wochentag wird der gewählte Status von Mitternacht bis zur Endzeit angezeigt."),
                        new KeyValuePair<string, string>("Wochentag und Startdatum:", "Gleich wie beim ersten Punkt, einfach erst ab dem gewählten Startdatum."),
						new KeyValuePair<string, string>("Wochentag, Zeit und Startdatum:", "Wie beim zweiten Punkt, einfach erst ab dem gewählten Startdatum."),
                        new KeyValuePair<string, string>("Wochentag, Zeit, Endzeit und Startdatum:", "Wie beim dritten Punkt, einfach erst ab dem gewählten Startdatum."),
                        new KeyValuePair<string, string>("Wochentag, Endzeit und Startdatum:", "Wie beim vierten Punkt, einfach erst ab dem gewählten Startdatum."),
                        new KeyValuePair<string, string>("Führungen:", "Wird keine Führung markiert ist die Regel für alle Führungen gültig, ansonsten nur für die markierten. " +
                            "Einzelne Führungen können mit gedrückter [ctrl]-Taste markiert werden. Auf die gleiche Art können die Führungen auch wieder abgewählt werden. ")
                    };
				}
			}

			public static class Settings
			{
				public const string Address = "Adresse";
				public const string AddNewRecipient = "EmpfängerIn hinzufügen:";
				public const string AddNewTime = "Zeit hinzufügen (Eingabe in hh:mm, z.B. \"09:15\"):";
				public const string AdminPassword = "Admin-Passwort";
				public const string BookingTimes = "Buchungszeiten";
                public const string BookingTimeOverrideRule = "Buchungszeiten übersteuern";
                public const string ChangePassword = "Passwort ändern:";
				public const string ConfirmationPageContent = "Inhalt der Bestätigungsseite";
				public const string Content = "Inhalt";
				public const string DeactivatedEvent = "[Deaktivierte Führung]";
				public const string DynamicContent = "Dynamische Textinhalte";
				public const string DynamicContentDescription = "Für alle Textinhalte können Platzhalter definiert werden, welche ggf. nur am gewünschten Tag angezeigt werden.";
				public const string EmailContent = "Inhalt des Bestätigungsmails";
				public const string CreateNewRule = "Neue Regel erstellen:";
				public const string CurrentPassword = "Momentanes Passwort:";
				public const string DateRangeRule = "Einzeldatum / Zeitperiode";
				public const string DisplayAlways = "An jedem Tag anzeigen";
				public const string EmailConfiguration = "E-Mail Konfiguration";
				public const string EmailSubject = "Betreff";
				public const string EventDurationRule = "Buchungszeitraum";
				public const string EventGroupRule = "Führungsgruppe";
				public const string GlobalSettings = "Globale Einstellungen";
				public const string MinimumDateRule = "Mindestdatum";
                public const string MultipleBookingRule = "Mehrfach Buchung";
                public const string PageTitle = "Einstellungen";
				public const string Recipients = "EmpfängerInnen von System-Meldungen";
				public const string RuleName = "Name";
				public const string RuleOverview = "Übersicht Buchungsregeln";
				public const string RuleSettings = "Konfigurationswerte";
				public const string RuleType = "Typ";
				public const string TextConfiguration = "Konfiguration Textinhalte";
				public const string TextConfigurationInfo = "Die Inhalte vom Bestätigungsmail und der Bestätigungsseite können dynamisch mittels Markdown konfiguriert werden:";
				public const string TextContentDisplayDay = "Wird angezeigt am";
				public const string TextContentKey = "Platzhalter";
				public const string TextContentValue = "Text";
				public const string Time = "Zeit";
				public const string WeeklyRule = "Wöchentliche Regel";

				public static readonly KeyValuePair<string, string>[] MarkdownInfos = new KeyValuePair<string, string>[]
				{
					new KeyValuePair<string, string>("# Titel 1", "Eine grosse Überschrift mit Text \"Titel 1\""),
					new KeyValuePair<string, string>("## Titel 2", "Eine kleinere Überschrift mit Text \"Titel 2\""),
					new KeyValuePair<string, string>("---", "Eine horizontale Linie"),
					new KeyValuePair<string, string>("\\\\", "Einen Zeilenumbruch"),
					new KeyValuePair<string, string>(ContentParser.EventNamePlaceholder, "Name der Führung"),
					new KeyValuePair<string, string>(ContentParser.EventDatePlaceholder, "Datum der Führung"),
					new KeyValuePair<string, string>(ContentParser.FirstNamePlaceholder, "Vorname der Person"),
					new KeyValuePair<string, string>(ContentParser.LastNamePlaceholder, "Nachname der Person"),
					new KeyValuePair<string, string>(ContentParser.EmailPlaceholder, "E-Mail-Adresse")
				};
			}
		}

		/// <summary>
		/// Strings used on the public pages.
		/// </summary>
		public static class Public
		{
			public const string AdditionalInformation = "Zusätzliche Angaben";
			public const string AvailabilityBooked = "Ausgebucht";
			public const string AvailabilityNotBookable = "Nicht buchbar";
			public const string BookingMessageGrade = "Bitte buchen Sie pro Klassenverband eine Führung.";
			public const string ConfirmationPageTitle = "Buchung erfolgreich!";
			public const string Date = "Datum";
			public const string Event = "Führung";
			public const string InputDescriptionDate = "Klicken Sie im untenstehenden Kalender einen freien Termin an. Bitte beachten Sie, dass die Verfügbarkeit je nach Führung variiert.";
			public const string InputDescriptionGrade = "z.B. \"Kindergarten, 5. Klasse\"";
			public const string InputDescriptionNumberOfKids = "min. 5 bis max. 25";
			public const string InputErrorCanton = "Bitte wählen Sie einen Kanton!";
			public const string InputErrorCaptcha = "Die eingegebene Antwort war nicht korrekt. Bitte versuchen Sie es erneut!";
			public const string InputErrorDate = "Bitte wählen Sie einen Termin!";
			public const string InputErrorEmail = "Bitte geben Sie eine gültige E-Mail-Adresse an!";
			public const string InputErrorEvent = "Bitte wählen Sie eine Führung!";
			public const string InputErrorGrade = "Bitte geben Sie eine Klassenbezeichnung an!";
			public const string InputErrorFirstName = "Bitte geben Sie einen Vornamen ein!";
			public const string InputErrorLastName = "Bitte geben Sie einen Nachnamen ein!";
			public const string InputErrorMaxLength100 = "Bitte geben Sie maximal 100 Zeichen ein!";
			public const string InputErrorMaxLength5000 = "Bitte geben Sie maximal 5'000 Zeichen ein!";
			public const string InputErrorNumberOfKids = "Bitte geben Sie eine gültige Anzahl Kinder an (5 - 25)!";
			public const string InputErrorPhone = "Bitte geben Sie eine gültige Telefonnummer an!";
			public const string InputErrorSchool = "Bitte geben Sie den Namen Ihrer Schule an!";
			public const string InputErrorTown = "Bitte geben Sie einen Wohnort an!";
			public const string InputErrorZipCode = "Bitte geben Sie eine gültige Postleitzahl an!";
			public const string InputLabelAddress = "Adresse";
			public const string InputLabelCanton = "Kanton*";
			public const string InputLabelCaptcha = "Sicherheitsfrage";
			public const string InputLabelEmail = "E-Mail*";
			public const string InputLabelFirstName = "Vorname*";
			public const string InputLabelGrade = "Klassenbezeichnung*";
			public const string InputLabelLastName = "Nachname*";
			public const string InputLabelNotes = "Vorwissen, Bemerkungen";
			public const string InputLabelNumberOfKids = "Anzahl Kinder*";
			public const string InputLabelPhone = "Telefon*";
			public const string InputLabelSchool = "Schule*";
			public const string InputLabelTown = "Ortschaft*";
			public const string InputLabelZipCode = "Postleitzahl";
			public const string NavigationPreviousMonth = "❮❮ Einen Monat zurück";
			public const string NavigationPreviousWeek = "❮ Eine Woche zurück";
			public const string NavigationNextWeek = "Eine Woche vor ❯";
			public const string NavigationNextMonth = "Einen Monat vor ❯❯";
			public const string PageTitle = "Buchungsportal für Führungen";
			public const string PersonalInformation = "Persönliche Angaben";
			public const string PleaseSelect = " - Bitte wählen - ";
			public const string PleaseSelectEvent = "Bitte wählen Sie eine Führung aus!";
			public const string Submit = "Abschicken";

			public static class Canton
			{
				public const string AI = "AI";
				public const string AR = "AR";
				public const string GR = "GR";
				public const string TG = "TG";
				public const string SG = "SG";
				public const string ZH = "ZH";
				public const string Other = "Anderer";
			}
		}
	}
}
