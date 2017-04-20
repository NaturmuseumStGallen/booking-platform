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
using System.Transactions;
using BookingPlatform.Backend.Configuration;

namespace BookingPlatform.Backend.DataAccess
{
	internal abstract class DbBaseDao<TEntity>
	{
		protected IList<TEntity> ExecuteMultiQuery(string sql, params SqlParameter[] parameters)
		{
			using (var transaction = new TransactionScope())
			using (var connection = NewSqlConnection())
			using (var command = new SqlCommand(sql, connection))
			{
				connection.Open();
				command.Parameters.AddRange(parameters);

				using (var reader = command.ExecuteReader())
				{
					var results = new List<TEntity>();

					while (reader.Read())
					{
						results.Add(MapFrom(reader));
					}

					transaction.Complete();

					return results;
				}
			}
		}

		protected void ExecuteNonQuery(string sql, params SqlParameter[] parameters)
		{
			using (var transaction = new TransactionScope())
			using (var connection = NewSqlConnection())
			using (var command = new SqlCommand(sql, connection))
			{
				connection.Open();

				command.Parameters.AddRange(parameters);
				command.ExecuteNonQuery();

				transaction.Complete();
			}
		}

		protected object ExecuteScalar(string sql, params SqlParameter[] parameters)
		{
			using (var transaction = new TransactionScope())
			using (var connection = NewSqlConnection())
			using (var command = new SqlCommand(sql, connection))
			{
				object result;

				connection.Open();
				command.Parameters.AddRange(parameters);
				result = command.ExecuteScalar();
				transaction.Complete();

				return result;
			}
		}

		protected TEntity ExecuteSingleQuery(string sql, params SqlParameter[] parameters)
		{
			using (var transaction = new TransactionScope())
			using (var connection = NewSqlConnection())
			using (var command = new SqlCommand(sql, connection))
			{
				connection.Open();
				command.Parameters.AddRange(parameters);

				using (var reader = command.ExecuteReader())
				{
					TEntity entity = default(TEntity);

					reader.Read();

					if (reader.HasRows)
					{
						entity = MapFrom(reader);
					}

					transaction.Complete();

					return entity;
				}
			}
		}

		protected SqlConnection NewSqlConnection()
		{
			var builder = new SqlConnectionStringBuilder();

			builder.DataSource = AppConfig.DataSource;
			builder.UserID = AppConfig.UserId;
			builder.Password = AppConfig.Password;
			builder.InitialCatalog = AppConfig.InitialCatalog;

			return new SqlConnection(builder.ToString());
		}

		protected abstract TEntity MapFrom(SqlDataReader reader);
	}
}
