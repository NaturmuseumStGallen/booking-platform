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

using System.Data.SqlClient;
using BookingPlatform.Backend.Entities;

namespace BookingPlatform.Backend.DataAccess
{
	internal class DbSettingsDao : DbBaseDao<Settings>
	{
		public Settings Get()
		{
			var sql = "SELECT * FROM Settings";

			return ExecuteSingleQuery(sql);
		}

		public void UpdateEmailContent(string title, string plaintext, string html)
		{
			var sql = @"
			UPDATE
				Settings
			SET
				EmailTitle = @EmailTitle,
				EmailHtmlContent = @EmailHtmlContent,
				EmailPlaintextContent = @EmailPlaintextContent";
			var parameters = new[]
			{
				new SqlParameter("@EmailTitle", title ?? string.Empty),
				new SqlParameter("@EmailHtmlContent", html ?? string.Empty),
				new SqlParameter("@EmailPlaintextContent", plaintext ?? string.Empty)
			};

			ExecuteNonQuery(sql, parameters);
		}

		public void UpdatePassword(string hash, string salt)
		{
			var sql = @"
			UPDATE
				Settings
			SET
				PasswordHash = @PasswordHash,
				PasswordSalt = @PasswordSalt";
			var parameters = new[]
			{
				new SqlParameter("@PasswordHash", hash),
				new SqlParameter("@PasswordSalt", salt)
			};

			ExecuteNonQuery(sql, parameters);
		}

		protected override Settings MapFrom(SqlDataReader reader)
		{
			var settings = new Settings();

			settings.EmailTitle = (string) reader[nameof(Settings.EmailTitle)];
			settings.EmailHtmlContent = (string) reader[nameof(Settings.EmailHtmlContent)];
			settings.EmailPlaintextContent = (string) reader[nameof(Settings.EmailPlaintextContent)];
			settings.PasswordHash = (string) reader[nameof(Settings.PasswordHash)];
			settings.PasswordSalt = (string) reader[nameof(Settings.PasswordSalt)];

			return settings;
		}
	}
}
