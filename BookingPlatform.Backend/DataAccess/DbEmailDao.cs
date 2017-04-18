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

using System.Collections.Generic;
using System.Data.SqlClient;
using BookingPlatform.Backend.Entities;

namespace BookingPlatform.Backend.DataAccess
{
	internal class DbEmailDao : DbBaseDao<EmailRecipient>
	{
		public void Delete(int id)
		{
			var sql = "DELETE FROM EmailRecipient WHERE Id = @Id";
			var parameter = new SqlParameter("@Id", id);

			ExecuteNonQuery(sql, parameter);
		}

		public IList<EmailRecipient> GetAll()
		{
			var sql = "SELECT * FROM EmailRecipient";

			return ExecuteMultiQuery(sql);
		}

		public void SaveNew(string email)
		{
			var sql = "INSERT INTO EmailRecipient(Address) VALUES (@Address)";
			var parameter = new SqlParameter("@Address", email);

			ExecuteNonQuery(sql, parameter);
		}

		protected override EmailRecipient MapFrom(SqlDataReader reader)
		{
			var recipient = new EmailRecipient();

			recipient.Id = (int) reader[nameof(EmailRecipient.Id)];
			recipient.Address = (string) reader[nameof(EmailRecipient.Address)];

			return recipient;
		}
	}
}
