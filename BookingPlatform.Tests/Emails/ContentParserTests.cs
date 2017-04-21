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
		private static readonly string NewLine = Environment.NewLine;

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
			var html2 = ContentParser.ToMarkup("Some Text" + NewLine + "# Hello World");

			Assert.AreEqual(@"<h1>Hello World</h1>", html1);
			Assert.AreEqual(@"<p>Some Text</p><h1>Hello World</h1>", html2);
		}

		[TestMethod]
		public void CreatesHeading2()
		{
			var html1 = ContentParser.ToMarkup("## Hello World");
			var html2 = ContentParser.ToMarkup("Some Text" + NewLine + "## Hello World");

			Assert.AreEqual(@"<h2>Hello World</h2>", html1);
			Assert.AreEqual(@"<p>Some Text</p><h2>Hello World</h2>", html2);
		}

		[TestMethod]
		public void CreatesHorizontalRule()
		{
			var html1 = ContentParser.ToMarkup("---");
			var html2 = ContentParser.ToMarkup("Some Text" + NewLine + "---");
			var html3 = ContentParser.ToMarkup("Some Text" + NewLine + "---" + NewLine + "And even more");

			Assert.AreEqual(@"<hr />", html1);
			Assert.AreEqual(@"<p>Some Text</p><hr />", html2);
			Assert.AreEqual(@"<p>Some Text</p><hr /><p>And even more</p>", html3);
		}

		[TestMethod]
		public void CreatesLineBreak()
		{
			var html = ContentParser.ToMarkup("Some text\\\\" + NewLine + "And even more" + NewLine + NewLine + @"\\" + NewLine + "A new paragraph");

			Assert.AreEqual(@"<p>Some text<br /> And even more</p><br /><p>A new paragraph</p>", html);
		}

		[TestMethod]
		public void CreatesLinks()
		{
			var html = ContentParser.ToMarkup(@"Some paragraph with a https://www.yoursite.net/link in it...");

			Assert.AreEqual("<p>Some paragraph with a <a href=\"https://www.yoursite.net/link\">https://www.yoursite.net/link</a> in it...</p>", html);
		}

		[TestMethod]
		public void ParagraphTest()
		{
			var markdown = "Some text here." + NewLine + "In the same paragraph." + NewLine + NewLine + "Then a new paragraph.";
			var expected = @"<p>Some text here. In the same paragraph.</p><p>Then a new paragraph.</p>";
			var html = ContentParser.ToMarkup(markdown);

			Assert.AreEqual(expected, html);
		}

		[TestMethod]
		public void RemovesMarkdownForPlaintext()
		{
			var plain = ContentParser.ToPlaintext("# Hello World" + NewLine + "## Blah 3" + NewLine + "---");
			var expected = "Hello World" + NewLine + "Blah 3" + NewLine + "---------------------------------------";

			Assert.AreEqual(expected, plain);
		}
	}
}
