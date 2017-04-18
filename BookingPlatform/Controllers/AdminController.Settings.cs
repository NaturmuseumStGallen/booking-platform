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
using System.Web.Mvc;
using BookingPlatform.Backend.Constants;
using BookingPlatform.Backend.DataAccess;
using BookingPlatform.Backend.Entities;
using BookingPlatform.Models;

namespace BookingPlatform.Controllers
{
	public partial class AdminController
	{
		[HttpGet]
		public ActionResult DeleteRecipient(int? id)
		{
			if (id.HasValue)
			{
				Database.Instance.DeleteEmailRecipient(id.Value);
			}

			return RedirectToAction("Settings");
		}

		[HttpGet]
		public ActionResult DeleteRule(int? id)
		{
			if (id.HasValue)
			{
				Database.Instance.DeleteRule(id.Value);
			}

			return RedirectToAction("Settings");
		}

		[HttpGet]
		public ActionResult DeleteTime(int? id)
		{
			if (id.HasValue)
			{
				Database.Instance.DeleteTime(id.Value);
			}

			return RedirectToAction("Settings");
		}

		[HttpPost]
		public ActionResult NewRecipient(string email)
		{
			if (ValidationUtility.IsValidEmail(email))
			{
				Database.Instance.SaveNewEmailRecipient(email);
			}

			return RedirectToAction("Settings");
		}

		[HttpPost]
		public ActionResult NewTime(string time)
		{
			if (ValidationUtility.IsValidTime(time))
			{
				Database.Instance.SaveNewTime(TimeSpan.Parse(time));
			}

			return RedirectToAction("Settings");
		}

		[HttpGet]
		public ActionResult RuleDetails(int? ruleId, RuleType? type)
		{
			if (!ruleId.HasValue && type.HasValue)
			{
				var model = ModelMapper.NewModelFor(type.Value);

				model.InitializeFor(type.Value);

				return View(model);
			}
			else if (ruleId.HasValue && Database.Instance.IsValidRuleId(ruleId.Value))
			{
				var ruleData = Database.Instance.GetRuleData(ruleId.Value);
				var model = ModelMapper.NewModelFor(ruleData.Type);

				model.InitializeFor(ruleData.Type);
				model.MapFromEntity(ruleData);

				return View(model);
			}

			return RedirectToAction("Settings");
		}

		[HttpPost]
		public ActionResult RuleDetails(AdminRuleDetailsModel model)
		{
			if (ModelState.IsValid)
			{
				var ruleData = model.RuleId.HasValue ? Database.Instance.GetRuleData(model.RuleId.Value) : ModelMapper.NewEntityFor(model.Type.Value);

				model.MapToEntity(ruleData);

				if (model.RuleId.HasValue)
				{
					Database.Instance.Update(ruleData);
				}
				else
				{
					Database.Instance.SaveNew(ruleData);
				}

				return RedirectToAction("Settings");
			}

			return View(model);
		}

		[HttpGet]
		public ActionResult Settings()
		{
			var model = new AdminSettingsModel();
			var settings = Database.Instance.GetSettings();

			model.MapFromEntity(settings);

			model.Recipients = Database.Instance.GetEmailRecipients();
			model.Rules = Database.Instance.GetRuleData();
			model.Times = Database.Instance.GetTimeData();

			return View(model);
		}

		[HttpPost]
		public ActionResult UpdateEmailContent(string title, string plaintext, string html)
		{
			if (ValidationUtility.AreNotNullOrWhitespace(title, plaintext, html))
			{
				Database.Instance.UpdateEmailContent(title, plaintext, html);
			}

			return RedirectToAction("Settings");
		}

		[HttpPost]
		public ActionResult UpdatePassword(string password)
		{
			if (ValidationUtility.IsValidPassword(password))
			{
				// TODO: Implement hashing & encryption, then save to DB...
				//Database.Instance.UpdatePassword(password);
				throw new NotImplementedException();
			}

			return RedirectToAction("Settings");
		}
	}
}