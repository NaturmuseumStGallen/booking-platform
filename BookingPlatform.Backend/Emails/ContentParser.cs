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

		public static string Replace(string text, IList<TextContent> contents)
		{
			var result = text ?? string.Empty;

			foreach (var content in contents)
			{
				var value = !content.DisplayDay.HasValue || content.DisplayDay == DateTime.Today.DayOfWeek ? content.Value : string.Empty;

				result.Replace(content.Key, value);
			}

			return result;
		}

		public static string ToMarkup(string markdown)
		{
			markdown = markdown ?? string.Empty;

			var html = new StringBuilder();
			var lines = markdown.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

			foreach (var line in lines)
			{
				if (line.StartsWith(HEADING_1))
				{
					html.AppendFormat("<h1>{0}</h1>", line.Replace(HEADING_1, string.Empty));
				}
				else if (line.StartsWith(HEADING_2))
				{
					html.AppendFormat("<h2>{0}</h2>", line.Replace(HEADING_2, string.Empty));
				}
				else if (line == HORIZONTAL_RULE)
				{
					html.Append("<hr />");
				}
				else if (line == NEW_LINE)
				{
					html.Append("<br />");
				}
				else if (markdown.Length > 0)
				{
					html.AppendFormat("<p>{0}</p>", AutoReplaceLinks(line));
				}
			}

			return html.ToString();
		}

		public static string ToPlaintext(string markdown)
		{
			markdown = markdown ?? string.Empty;

			var plain = new StringBuilder();
			var lines = markdown.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

			foreach (var line in lines)
			{
				var textOnly = line;

				textOnly = textOnly.Replace(HEADING_2, string.Empty);
				textOnly = textOnly.Replace(HEADING_1, string.Empty);
				textOnly = textOnly.Replace(HORIZONTAL_RULE, string.Empty);
				textOnly = textOnly.Replace(NEW_LINE, string.Empty);

				if (markdown.Length > 0 && line != HORIZONTAL_RULE)
				{
					plain.AppendFormat("{0}{1}", textOnly, Environment.NewLine);
				}
			}

			return plain.ToString();
		}

		private static string AutoReplaceLinks(string markdown)
		{
			var tokens = markdown.Split(' ');

			foreach (var token in tokens)
			{
				if (Uri.IsWellFormedUriString(token, UriKind.Absolute))
				{
					markdown = markdown.Replace(token, String.Format("<a href=\"{0}\">{1}</a>", token, token));
				}
			}

			return markdown;
		}
	}
}
