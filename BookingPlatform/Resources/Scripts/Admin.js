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

var admin = admin || {};

$(document).ready(function () {
    attachSafetyHandlers();

    function attachSafetyHandlers() {
        var elements = $('[data-safety-message');

        $.each(elements, function (index, element) {
            var onclick = $(element).onclick;

            $(element).click(function (event) {
                safetyHandler(element, onclick, event);
            });
        });
    }

    function safetyHandler(element, onclick, event) {
        if (window.confirm($(element).attr('data-safety-message'))) {
            onclick.call(element, event);
            
            return true;
        }

        event.preventDefault();

        return false;
    }
});