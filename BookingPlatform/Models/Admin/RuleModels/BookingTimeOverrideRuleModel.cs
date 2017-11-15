﻿/*
 * Copyright (C) 2017 Naturmuseum St. Gallen
 *  > https://github.com/NaturmuseumStGallen
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BookingPlatform.Backend.Constants;
using BookingPlatform.Backend.Entities;
using BookingPlatform.Constants;
using System.Web.Mvc;

namespace BookingPlatform.Models
{
    public class BookingTimeOverrideRuleModel : AdminRuleDetailsModel, IValidatableObject
    {
        public BookingTimeOverrideRuleModel()
        {
            BookingTimes = new List<string>();
        }

        public override RuleType Type => RuleType.BookingTimeOverride;
        public int? Id { get; set; }

        [Required(ErrorMessage = Strings.Admin.RuleDetails.InputErrorEvent)]
        public int? EventId { get; set; }

        public IList<Event> AvailableEvents { get; set; }

        public IList<string> BookingTimes { get; set; }

        public IEnumerable<SelectListItem> EventListItems
        {
            get
            {
                foreach (var @event in AvailableEvents)
                {
                    yield return new SelectListItem { Text = @event.Name, Value = @event.Id.ToString(), Selected = @event.Id == EventId };
                }
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!BookingTimes.Any())
            {
                results.Add(new ValidationResult(Strings.Admin.RuleDetails.InputErrorTime, new[] { nameof(BookingTimes) }));
            }
            else
            {
                foreach(var time in BookingTimes)
                {
                    TimeSpan parsedTime;
                    if (!TimeSpan.TryParse(time, out parsedTime))
                    {
                        results.Add(new ValidationResult(Strings.Admin.RuleDetails.InputErrorTime, new[] { nameof(BookingTimes) }));
                    }
                }
            }

            if (!EventId.HasValue)
            {
                results.Add(new ValidationResult(Strings.Admin.RuleDetails.InputErrorEvent, new[] { nameof(EventId) }));
            }

            if (!results.Any())
            {
                results.Add(ValidationResult.Success);
            }

            return results;
        }
    }
}