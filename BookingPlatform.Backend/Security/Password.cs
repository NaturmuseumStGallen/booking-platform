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
using System.Security.Cryptography;
using System.Text;

namespace BookingPlatform.Backend.Security
{
	/// <remarks>
	/// Hashing algorithm implemented according to the following suggestions:
	/// - https://crackstation.net/hashing-security.htm
	/// - http://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129
	/// </remarks>
	public static class Password
	{
		internal const int PASSWORD_HASH_SIZE = 20;
		internal const int SALT_HASH_SIZE = 16;

		public static bool AreEqual(string hash1, string hash2)
		{
			var hash1Bytes = ToByteArray(hash1);
			var hash2Bytes = ToByteArray(hash2);

			for (int index = 0; index < hash1Bytes.Length; index++)
			{
				var equal = (hash1Bytes[index] ^ hash2Bytes[index]) == 0;

				if (!equal)
				{
					return false;
				}
			}

			return true;
		}

		public static string ComputeHash(string password, string salt)
		{
			var passwordBytes = Encoding.UTF8.GetBytes(password);
			var saltBytes = ToByteArray(salt);

			using (var algorithm = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000))
			{
				var hash = algorithm.GetBytes(PASSWORD_HASH_SIZE);

				return ToByteString(hash);
			}
		}

		public static string GenerateSalt()
		{
			var saltBytes = new byte[SALT_HASH_SIZE];

			using (var provider = new RNGCryptoServiceProvider())
			{
				provider.GetBytes(saltBytes);
			}

			return ToByteString(saltBytes);
		}

		internal static byte[] ToByteArray(string byteString)
		{
			var bytes = new byte[byteString.Length / 2];

			for (var index = 0; index < byteString.Length; index += 2)
			{
				var @byte = byteString.Substring(index, 2);

				bytes[index / 2] = Convert.ToByte(@byte, 16);
			}

			return bytes;
		}

		internal static string ToByteString(byte[] bytes)
		{
			var builder = new StringBuilder();

			foreach (var @byte in bytes)
			{
				builder.Append(@byte.ToString("x2"));
			}

			return builder.ToString();
		}
	}
}
