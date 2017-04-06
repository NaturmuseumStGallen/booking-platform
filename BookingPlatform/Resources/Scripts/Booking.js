/*
 * Copyright (C) 2017 Naturmuseum St. Gallen
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

var booking = booking || {};

booking.selectDate = function (td) {
    var date = $(td).attr('data-booking-date');

    $('[data-booking-date]').val(date);

    if ($('.field-validation-error').is(':visible')) {
        $('form').valid();
    }

    $('td:contains("X")').css('background-image', 'none');
    $('td:contains("X")').text('');
    $(td).css('background-image', 'repeating-linear-gradient(135deg, rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0) 2%)');
    $(td).text('X');
}