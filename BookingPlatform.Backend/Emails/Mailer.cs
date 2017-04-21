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
using System.Linq;
using System.Text;
using BookingPlatform.Backend.Configuration;
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
			var plain = new StringBuilder();
			var html = new StringBuilder();

			foreach (var propertyInfo in typeof(Booking).GetProperties())
			{
				if (propertyInfo.CanRead)
				{
					var name = propertyInfo.Name;
					var value = propertyInfo.GetValue(booking);

					value = value is Event ? (value as Event).Name : value;

					plain.AppendFormat("{0}: {1}{2}", name, value, Environment.NewLine);
					html.AppendFormat("<p>{0}: {1}</p>", name, value);
				}
			}

			message.From = new EmailAddress(AppConfig.EmailSenderAddress);
			message.Subject = "New Booking";
			message.PlainTextContent = plain.ToString();
			message.HtmlContent = html.ToString();

			foreach (var recipient in recipients)
			{
				message.AddTo(new EmailAddress(recipient.Address));
			}

			if (recipients.Any())
			{
				client.SendEmailAsync(message);
			}
		}
	}
}
