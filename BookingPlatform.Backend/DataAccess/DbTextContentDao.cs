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
using System.Data.SqlClient;
using BookingPlatform.Backend.Entities;

namespace BookingPlatform.Backend.DataAccess
{
	internal class DbTextContentDao : DbBaseDao<TextContent>
	{
		public void Delete(int id)
		{
			var sql = "DELETE FROM TextContent WHERE Id = @Id";
			var parameter = new SqlParameter("@Id", id);

			ExecuteNonQuery(sql, parameter);
		}

		public IList<TextContent> GetAll()
		{
			var sql = "SELECT * FROM TextContent";

			return ExecuteMultiQuery(sql);
		}

		public void SaveNew(TextContent content)
		{
			var sql = @"
			INSERT INTO
				TextContent([Key], [Value], DisplayDay)
			VALUES
				(@Key, @Value, @DisplayDay)";
			var parameters = new[]
			{
				new SqlParameter("@Key", content.Key),
				new SqlParameter("@Value", content.Value),
				new SqlParameter("@DisplayDay", (object) content.DisplayDay ?? DBNull.Value),
			};

			ExecuteNonQuery(sql, parameters);
		}

		protected override TextContent MapFrom(SqlDataReader reader)
		{
			var content = new TextContent();

			content.Id = (int) reader[nameof(TextContent.Id)];
			content.Key = (string) reader[nameof(TextContent.Key)];
			content.Value = (string) reader[nameof(TextContent.Value)];
			content.DisplayDay = reader[nameof(TextContent.DisplayDay)] as DayOfWeek?;

			return content;
		}
	}
}
