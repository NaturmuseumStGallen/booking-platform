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
using System.Linq;
using BookingPlatform.Backend.Entities;

namespace BookingPlatform.Backend.DataAccess
{
	internal class DbEventDao : DbBaseDao<Event>
	{
		public void Deactivate(int id)
		{
			var sql = "UPDATE [Event] SET IsActive = 0 WHERE Id = @Id";
			var parameter = new SqlParameter("@Id", id);

			ExecuteNonQuery(sql, parameter);
		}

		public bool Exists(int id)
		{
			var sql = "SELECT COUNT(*) FROM [Event] WHERE Id = @Id";
			var parameter = new SqlParameter("@Id", id);

			return Convert.ToInt32(ExecuteScalar(sql, parameter)) == 1;
		}

		public int GetActiveCount()
		{
			var sql = "SELECT COUNT(*) FROM [Event] WHERE IsActive = 1";

			return Convert.ToInt32(ExecuteScalar(sql));
		}

		public IList<Event> GetAllActive()
		{
			var sql = "SELECT * FROM [Event] WHERE IsActive = 1";

			return ExecuteMultiQuery(sql);
		}

		public Event GetBy(int id)
		{
			var sql = "SELECT * FROM [Event] WHERE Id = @Id";
			var parameter = new SqlParameter("@Id", id);

			return ExecuteSingleQuery(sql, parameter);
		}

		public IList<Event> GetBy(IList<int> eventIds)
		{
			var sql = "SELECT * FROM [Event] WHERE IsActive = 1 AND Id IN (%%PARAM_LIST%%)";
			var sqlParamList = String.Empty;
			var parameters = new List<SqlParameter>();
			var ids = eventIds.Distinct().ToList();

			foreach (var id in ids)
			{
				var paramId = "@Id" + ids.IndexOf(id);

				sqlParamList += paramId + (ids.Last() == id ? string.Empty : ", ");
				parameters.Add(new SqlParameter(paramId, id));
			}

			sql = sql.Replace("%%PARAM_LIST%%", sqlParamList);

			return ExecuteMultiQuery(sql, parameters.ToArray());
		}

		public void SaveNew(Event @event)
		{
			var sql = @"
			INSERT INTO
				[Event](IsActive, Name, ColorComponentBlue, ColorComponentGreen, ColorComponentRed)
			VALUES
				(@IsActive, @Name, @ColorComponentBlue, @ColorComponentGreen, @ColorComponentRed)";
			var parameters = new[]
			{
				new SqlParameter("@IsActive", @event.IsActive),
				new SqlParameter("@Name", @event.Name),
				new SqlParameter("@ColorComponentBlue", @event.ColorComponentBlue),
				new SqlParameter("@ColorComponentGreen", @event.ColorComponentGreen),
				new SqlParameter("@ColorComponentRed", @event.ColorComponentRed)
			};

			ExecuteNonQuery(sql, parameters);
		}

		public void Update(Event @event)
		{
			var sql = @"
			UPDATE
				[Event]
			SET
				IsActive = @IsActive,
				Name = @Name,
				ColorComponentBlue = @ColorComponentBlue,
				ColorComponentGreen = @ColorComponentGreen,
				ColorComponentRed = @ColorComponentRed
			WHERE
				Id = @Id";
			var parameters = new[]
			{
				new SqlParameter("@Id", @event.Id),
				new SqlParameter("@IsActive", @event.IsActive),
				new SqlParameter("@Name", @event.Name),
				new SqlParameter("@ColorComponentBlue", @event.ColorComponentBlue),
				new SqlParameter("@ColorComponentGreen", @event.ColorComponentGreen),
				new SqlParameter("@ColorComponentRed", @event.ColorComponentRed)
			};

			ExecuteNonQuery(sql, parameters);
		}

		protected override Event MapFrom(SqlDataReader reader)
		{
			var @event = new Event();

			@event.Id = (int) reader[nameof(Event.Id)];
			@event.IsActive = (bool) reader[nameof(Event.IsActive)];
			@event.Name = (string) reader[nameof(Event.Name)];
			@event.ColorComponentBlue = (int) reader[nameof(Event.ColorComponentBlue)];
			@event.ColorComponentGreen = (int) reader[nameof(Event.ColorComponentGreen)];
			@event.ColorComponentRed = (int) reader[nameof(Event.ColorComponentRed)];

			return @event;
		}
	}
}
