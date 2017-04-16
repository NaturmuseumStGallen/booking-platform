﻿/*
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

namespace BookingPlatform.Backend.Entities
{
	public class Booking
	{
		public string Address { get; set; }
		public DateTime Date { get; set; }
		public string Email { get; set; }
		public Event Event { get; set; }
		public int? EventId { get; set; }
		public string FirstName { get; set; }
		public string Grade { get; set; }
		public int? Id { get; set; }
		public bool IsActive { get; set; }
		public string LastName { get; set; }
		public string Notes { get; set; }
		public int NumberOfKids { get; set; }
		public string Phone { get; set; }
		public string School { get; set; }
		public string Town { get; set; }
		public int? ZipCode { get; set; }
	}
}
