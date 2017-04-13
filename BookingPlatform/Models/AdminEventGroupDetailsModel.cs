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
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BookingPlatform.Backend.Entities;
using BookingPlatform.Constants;

namespace BookingPlatform.Models
{
	public class AdminEventGroupDetailsModel
	{
		public AdminEventGroupDetailsModel()
		{
			AvailableEvents = new List<Event>();
			SelectedEvents = new List<Event>();
		}

		[Required(ErrorMessage = Strings.Admin.EventGroupDetails.InputErrorName)]
		[MaxLength(100, ErrorMessage = Strings.Admin.EventGroupDetails.InputErrorMaxLength100)]
		public string Name { get; set; }

		[Required(ErrorMessage = Strings.Admin.EventGroupDetails.InputErrorEvents)]
		public IList<int> EventIds { get; set; }

		public int? Id { get; set; }
		public bool IsNew { get; set; }
		public IList<Event> AvailableEvents { get; set; }
		public IList<Event> SelectedEvents { get; set; }

		public MultiSelectList Events
		{
			get { return new MultiSelectList(AvailableEvents, nameof(Event.Id), nameof(Event.Name), SelectedEvents); }
		}
	}
}