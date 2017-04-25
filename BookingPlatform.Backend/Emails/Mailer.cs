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
using System.Linq;
using System.Text;
using BookingPlatform.Backend.Configuration;
using BookingPlatform.Backend.Constants;
using BookingPlatform.Backend.DataAccess;
using BookingPlatform.Backend.Entities;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BookingPlatform.Backend.Emails
{
	public static class Mailer
	{
		public static void SendConfirmationMail(Booking booking)
		{
			var client = new SendGridClient(AppConfig.SendGridApiKey);
			var message = new SendGridMessage();
			var settings = Database.Instance.GetSettings();
			var textContent = Database.Instance.GetTextContent();
			var subject = ContentParser.ReplacePlaceholders(settings.EmailSubject, textContent, booking);
			var plaintext = ContentParser.ReplacePlaceholders(settings.EmailContent, textContent, booking);
			var html = ContentParser.ReplacePlaceholders(settings.EmailContent, textContent, booking);

			message.From = new EmailAddress(AppConfig.EmailSenderAddress);
			message.Subject = subject;
			message.PlainTextContent = ContentParser.ToPlaintext(plaintext);
			message.HtmlContent = ContentParser.ToHtml(html);
			message.AddTo(new EmailAddress(booking.Email, String.Format("{0} {1}", booking.FirstName, booking.LastName)));

			if (!String.IsNullOrWhiteSpace(message.Subject) && !String.IsNullOrWhiteSpace(message.HtmlContent))
			{
				client.SendEmailAsync(message);
			}
		}

		public static void SendNewBookingAlert(Booking booking)
		{
			var client = new SendGridClient(AppConfig.SendGridApiKey);
			var message = new SendGridMessage();
			var recipients = Database.Instance.GetEmailRecipients();

			message.From = new EmailAddress(AppConfig.EmailSenderAddress);
			message.Subject = I18n.SystemEmail.Subject;
			message.PlainTextContent = BuildPlainTextContentFor(booking);
			message.HtmlContent = BuildHtmlContentFor(booking);

			foreach (var recipient in recipients)
			{
				message.AddTo(new EmailAddress(recipient.Address));
			}

			if (recipients.Any())
			{
				client.SendEmailAsync(message);
			}
		}

		private static string BuildPlainTextContentFor(Booking booking)
		{
			var plain = new StringBuilder();

			plain.Append(I18n.SystemEmail.NewBookingParagraph + Environment.NewLine);
			plain.AppendFormat("{0}: {1}{2}", I18n.SystemEmail.BookingDate, booking.Date, Environment.NewLine);
			plain.AppendFormat("{0}: {1}{2}", I18n.SystemEmail.Event, booking.Event.Name, Environment.NewLine);
			plain.AppendFormat("{0}: {1}{2}", I18n.SystemEmail.Email, booking.Email, Environment.NewLine);
			plain.AppendFormat("{0}: {1}{2}", I18n.SystemEmail.FirstName, booking.FirstName, Environment.NewLine);
			plain.AppendFormat("{0}: {1}{2}", I18n.SystemEmail.LastName, booking.LastName, Environment.NewLine);
			plain.AppendFormat("{0}: {1}{2}", I18n.SystemEmail.School, booking.School, Environment.NewLine);
			plain.AppendFormat("{0}: {1}{2}", I18n.SystemEmail.Address, booking.Address, Environment.NewLine);
			plain.AppendFormat("{0}: {1}{2}", I18n.SystemEmail.ZipCode, booking.ZipCode, Environment.NewLine);
			plain.AppendFormat("{0}: {1}{2}", I18n.SystemEmail.Town, booking.Town, Environment.NewLine);
			plain.AppendFormat("{0}: {1}{2}", I18n.SystemEmail.Canton, booking.Canton, Environment.NewLine);
			plain.AppendFormat("{0}: {1}{2}", I18n.SystemEmail.Phone, booking.Phone, Environment.NewLine);
			plain.AppendFormat("{0}: {1}{2}", I18n.SystemEmail.NumberOfKids, booking.NumberOfKids, Environment.NewLine);
			plain.AppendFormat("{0}: {1}{2}", I18n.SystemEmail.Grade, booking.Grade, Environment.NewLine);
			plain.Append(Environment.NewLine);
			plain.AppendFormat("{0}:{1}{2}", I18n.SystemEmail.Notes, Environment.NewLine, booking.Notes);

			return plain.ToString();
		}

		private static string BuildHtmlContentFor(Booking booking)
		{
			var html = new StringBuilder();

			html.AppendFormat("<p>{0}</p>", I18n.SystemEmail.NewBookingParagraph + Environment.NewLine);
			html.Append("<p>");
			html.AppendFormat("{0}: {1}<br />", I18n.SystemEmail.BookingDate, booking.Date);
			html.AppendFormat("{0}: {1}<br />", I18n.SystemEmail.Event, booking.Event.Name);
			html.AppendFormat("{0}: {1}<br />", I18n.SystemEmail.Email, booking.Email);
			html.AppendFormat("{0}: {1}<br />", I18n.SystemEmail.FirstName, booking.FirstName);
			html.AppendFormat("{0}: {1}<br />", I18n.SystemEmail.LastName, booking.LastName);
			html.AppendFormat("{0}: {1}<br />", I18n.SystemEmail.School, booking.School);
			html.AppendFormat("{0}: {1}<br />", I18n.SystemEmail.Address, booking.Address);
			html.AppendFormat("{0}: {1}<br />", I18n.SystemEmail.ZipCode, booking.ZipCode);
			html.AppendFormat("{0}: {1}<br />", I18n.SystemEmail.Town, booking.Town);
			html.AppendFormat("{0}: {1}<br />", I18n.SystemEmail.Canton, booking.Canton);
			html.AppendFormat("{0}: {1}<br />", I18n.SystemEmail.Phone, booking.Phone);
			html.AppendFormat("{0}: {1}<br />", I18n.SystemEmail.NumberOfKids, booking.NumberOfKids);
			html.AppendFormat("{0}: {1}<br />", I18n.SystemEmail.Grade, booking.Grade);
			html.Append("</p>");
			html.AppendFormat("<p>{0}:<br />{1}</p>", I18n.SystemEmail.Notes, booking.Notes);

			return html.ToString();
		}
	}
}
