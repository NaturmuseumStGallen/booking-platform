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
using System.Collections.Generic;
using System.Text;
using BookingPlatform.Backend.Entities;

namespace BookingPlatform.Backend.Emails
{
	public static class ContentParser
	{
		private const string HEADING_1 = "# ";
		private const string HEADING_2 = "## ";
		private const string HORIZONTAL_RULE = "---";
		private const string NEW_LINE = @"\\";

		public const string EmailPlaceholder = "%%Email%%";
		public const string EventNamePlaceholder = "%%EventName%%";
		public const string EventDatePlaceholder = "%%EventDate%%";
		public const string FirstNamePlaceholder = "%%FirstName%%";
		public const string LastNamePlaceholder = "%%LastName%%";

		public static string ReplacePlaceholders(string text, IList<TextContent> contents, Booking booking = null)
		{
			var result = text ?? string.Empty;

			foreach (var content in contents)
			{
				var value = !content.DisplayDay.HasValue || content.DisplayDay == DateTime.Today.DayOfWeek ? content.Value : string.Empty;

				result = result.Replace(content.Key, value);
			}

			if (booking != null)
			{
				result = result.Replace(EmailPlaceholder, booking.Email);
				result = result.Replace(EventNamePlaceholder, booking.Event.Name);
				result = result.Replace(EventDatePlaceholder, booking.Date.ToLongDateString() + " " + booking.Date.TimeOfDay.ToString("hh\\:mm"));
				result = result.Replace(FirstNamePlaceholder, booking.FirstName);
				result = result.Replace(LastNamePlaceholder, booking.LastName);
			}

			return result;
		}

		public static string ToHtml(string markdown)
		{
			markdown = markdown ?? string.Empty;

			var html = new StringBuilder();
			var lines = markdown.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
			var paragraph = false;
			Action closeParagraphIfNecessary = () => { if (paragraph) { html.Append("</p>"); paragraph = false; } };

			foreach (var line in lines)
			{
				if (line.StartsWith(HEADING_1))
				{
					closeParagraphIfNecessary();
					html.AppendFormat("<h1>{0}</h1>", line.Replace(HEADING_1, string.Empty));
				}
				else if (line.StartsWith(HEADING_2))
				{
					closeParagraphIfNecessary();
					html.AppendFormat("<h2>{0}</h2>", line.Replace(HEADING_2, string.Empty));
				}
				else if (line == HORIZONTAL_RULE)
				{
					closeParagraphIfNecessary();
					html.Append("<hr />");
				}
				else if (line == NEW_LINE)
				{
					closeParagraphIfNecessary();
					html.Append("<br />");
				}
				else if (line.Length > 0)
				{
					var isExistingParagraph = paragraph;
					var htmlLine = AutoReplaceLinks(line).Replace(NEW_LINE, "<br />");

					if (!paragraph)
					{
						html.Append("<p>");
						paragraph = true;
					}

					html.AppendFormat("{0}{1}", isExistingParagraph ? " " : string.Empty, htmlLine);
				}
				else
				{
					closeParagraphIfNecessary();
				}
			}

			closeParagraphIfNecessary();

			return html.ToString();
		}

		public static string ToPlaintext(string markdown)
		{
			markdown = markdown ?? string.Empty;

			var plain = new StringBuilder();
			var lines = markdown.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

			for (var index = 0; index < lines.Length; index++)
			{
				var textOnly = lines[index];

				textOnly = textOnly.Replace(HEADING_2, string.Empty);
				textOnly = textOnly.Replace(HEADING_1, string.Empty);
				textOnly = textOnly.Replace(HORIZONTAL_RULE, "---------------------------------------");
				textOnly = textOnly.Replace(NEW_LINE, string.Empty);

				if (markdown.Length > 0)
				{
					plain.AppendFormat("{0}{1}", textOnly, index == lines.Length - 1 ? string.Empty : Environment.NewLine);
				}
			}

			return plain.ToString();
		}

		private static string AutoReplaceLinks(string markdown)
		{
			var html = markdown ?? string.Empty;
			var tokens = html.Split(' ');

			foreach (var token in tokens)
			{
				if (Uri.IsWellFormedUriString(token, UriKind.Absolute))
				{
					html = html.Replace(token, String.Format("<a href=\"{0}\">{1}</a>", token, token));
				}
			}

			return html;
		}
	}
}
