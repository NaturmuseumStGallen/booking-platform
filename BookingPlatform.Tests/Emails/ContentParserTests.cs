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
using BookingPlatform.Backend.Emails;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookingPlatform.Tests.Emails
{
	[TestClass]
	public class ContentParserTests
	{
		[TestMethod]
		public void DoesNotReturnNull()
		{
			var html = ContentParser.ToMarkup(null);
			var plain = ContentParser.ToPlaintext(null);

			Assert.IsNotNull(html);
			Assert.IsNotNull(plain);
		}

		[TestMethod]
		public void ReturnsEmptyString()
		{
			var html = ContentParser.ToMarkup("");
			var plain = ContentParser.ToPlaintext("");

			Assert.AreEqual(string.Empty, html);
			Assert.AreEqual(string.Empty, plain);
		}

		[TestMethod]
		public void CreatesHeading1()
		{
			var html1 = ContentParser.ToMarkup("# Hello World");
			var html2 = ContentParser.ToMarkup("Some Text" + Environment.NewLine + "# Hello World");

			Assert.AreEqual(@"<h1>Hello World</h1>", html1);
			Assert.AreEqual(@"<p>Some Text</p><h1>Hello World</h1>", html2);
		}

		[TestMethod]
		public void CreatesHeading2()
		{
			var html1 = ContentParser.ToMarkup("## Hello World");
			var html2 = ContentParser.ToMarkup("Some Text" + Environment.NewLine + "## Hello World");

			Assert.AreEqual(@"<h2>Hello World</h2>", html1);
			Assert.AreEqual(@"<p>Some Text</p><h2>Hello World</h2>", html2);
		}

		[TestMethod]
		public void CreatesHorizontalRule()
		{
			var html1 = ContentParser.ToMarkup("---");
			var html2 = ContentParser.ToMarkup("Some Text" + Environment.NewLine + "---");
			var html3 = ContentParser.ToMarkup("Some Text" + Environment.NewLine + "---" + Environment.NewLine + "And even more");

			Assert.AreEqual(@"<hr />", html1);
			Assert.AreEqual(@"<p>Some Text</p><hr />", html2);
			Assert.AreEqual(@"<p>Some Text</p><hr /><p>And even more</p>", html3);
		}

		[TestMethod]
		public void CreatesLineBreak()
		{
			var html = ContentParser.ToMarkup("Some text" + Environment.NewLine + @"\\" + Environment.NewLine + "And even more");

			Assert.AreEqual(@"<p>Some text</p><br /><p>And even more</p>", html);
		}

		[TestMethod]
		public void CreatesLinks()
		{
			var html = ContentParser.ToMarkup(@"Some paragraph with a https://www.yoursite.net/link in it...");

			Assert.AreEqual("<p>Some paragraph with a <a href=\"https://www.yoursite.net/link\">https://www.yoursite.net/link</a> in it...</p>", html);
		}

		[TestMethod]
		public void RemovesMarkdownForPlaintext()
		{
			var plain = ContentParser.ToPlaintext("# Hello World" + Environment.NewLine + "## Blah 3" + Environment.NewLine + "---");
			var expected = "Hello World" + Environment.NewLine + "Blah 3" + Environment.NewLine;

			Assert.AreEqual(expected, plain);
		}
	}
}
