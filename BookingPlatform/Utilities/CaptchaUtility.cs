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
using System.Drawing;
using System.Web;

namespace BookingPlatform.Utilities
{
	public static class CaptchaUtility
	{
		private const string CAPTCHA = "Captcha";

		public static Captcha GenerateAndStoreInSession()
		{
			var captcha = GenerateNew();

			HttpContext.Current.Session[CAPTCHA] = captcha;

			return captcha;
		}

		public static bool TryGetFromSession(out Captcha captcha)
		{
			captcha = HttpContext.Current.Session[CAPTCHA] as Captcha;

			return captcha != null;
		}

		public static Captcha GenerateNew()
		{
			var random = new Random();
			var a = random.Next(-20, 20);
			var b = random.Next(-20, 20);
			var result = a + b;
			var question = String.Format("{0} + {1} = ?", a < 0 ? "(" + a + ")" : a.ToString(), b < 0 ? "(" + b + ")" : b.ToString());

			var bitmap = new Bitmap(1, 1);
			var brush = new SolidBrush(Color.Gray);
			var font = new Font(FontFamily.GenericMonospace, 12f);
			var graphics = Graphics.FromImage(bitmap);
			var textSize = graphics.MeasureString(question, font);

			bitmap.Dispose();
			graphics.Dispose();

			bitmap = new Bitmap((int)Math.Ceiling(textSize.Width), (int)Math.Ceiling(textSize.Height));
			graphics = Graphics.FromImage(bitmap);

			graphics.Clear(Color.WhiteSmoke);
			graphics.DrawString(question, font, brush, 0, 0);
			graphics.Save();

			brush.Dispose();
			graphics.Dispose();

			var imageBytes = (byte[]) new ImageConverter().ConvertTo(bitmap, typeof(byte[]));

			return new Captcha
			{
				ImageData = Convert.ToBase64String(imageBytes),
				Solution = result
			};
		}

		[Serializable]
		public class Captcha
		{
			public string ImageData { get; set; }
			public int Solution { get; set; }
		}
	}
}