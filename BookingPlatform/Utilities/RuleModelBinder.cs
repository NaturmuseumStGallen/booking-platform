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
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using BookingPlatform.Backend.Constants;
using BookingPlatform.Models;

namespace BookingPlatform.Utilities
{
	/// <summary>
	/// An extension of the <c>DefaultModelBinder</c> to automatically detect and instantiate the right specialization
	/// of <c>AdminRuleDetailsModel</c>, the base class for all rule models.
	/// </summary>
	public class RuleModelBinder : DefaultModelBinder
	{
		protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
		{
			if (modelType == typeof(AdminRuleDetailsModel))
			{
				var type = GetRuleType(controllerContext.HttpContext.Request.Form);
				var model = ModelMapper.NewModelFor(type);

				bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, model.GetType());

				return model;
			}

			return base.CreateModel(controllerContext, bindingContext, modelType);
		}

		private RuleType GetRuleType(NameValueCollection form)
		{
			var modelTypeKey = nameof(AdminRuleDetailsModel.Type);

			if (form.AllKeys.All(k => k != modelTypeKey))
			{
				throw new InvalidOperationException("Rule models need to specify their type as hidden form field!");
			}

			return (RuleType) Enum.Parse(typeof(RuleType), form[modelTypeKey]);
		}
	}
}