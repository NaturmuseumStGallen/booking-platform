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

using BookingPlatform.Backend.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookingPlatform.Tests.Security
{
	[TestClass]
	public class PasswordTests
	{
		[TestMethod]
		public void ByteAndStringConversionTest()
		{
			var hex = "73a7b6dc8d1d75a0352c3ba917266afa";
			var array = Password.ToByteArray(hex);
			var byteString = Password.ToByteString(array);

			Assert.IsTrue(hex == byteString);
			Assert.IsTrue(hex.Length == array.Length * 2);
		}

		[TestMethod]
		public void SaltGenerationTest()
		{
			var saltLength = Password.SALT_HASH_SIZE * 2;
			var salt1 = Password.GenerateSalt();
			var salt2 = Password.GenerateSalt();
			var salt3 = Password.GenerateSalt();

			Assert.IsTrue(salt1.Length == saltLength);
			Assert.IsTrue(salt2.Length == saltLength);
			Assert.IsTrue(salt3.Length == saltLength);

			Assert.AreNotEqual(salt1, salt2);
			Assert.AreNotEqual(salt1, salt3);
			Assert.AreNotEqual(salt2, salt3);
		}

		[TestMethod]
		public void PasswordHashingTest()
		{
			var passwordLength = Password.PASSWORD_HASH_SIZE * 2;
			var salt = "73a7b6dc8d1d75a0352c3ba917266afa";
			var password = "SomeRandomPassword_1234";

			var hash1 = Password.ComputeHash(password, salt);
			var hash2 = Password.ComputeHash(password, salt);
			var hash3 = Password.ComputeHash(password, salt);
			var hash4 = Password.ComputeHash(password + "5", salt);

			Assert.IsTrue(hash1.Length == passwordLength);
			Assert.IsTrue(hash2.Length == passwordLength);
			Assert.IsTrue(hash3.Length == passwordLength);
			Assert.IsTrue(hash4.Length == passwordLength);

			Assert.IsTrue(Password.AreEqual(hash1, hash2));
			Assert.IsTrue(Password.AreEqual(hash1, hash3));
			Assert.IsTrue(Password.AreEqual(hash2, hash3));

			Assert.IsFalse(Password.AreEqual(hash1, hash4));
			Assert.IsFalse(Password.AreEqual(hash2, hash4));
			Assert.IsFalse(Password.AreEqual(hash3, hash4));
		}
	}
}
