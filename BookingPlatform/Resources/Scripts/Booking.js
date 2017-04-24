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

var booking = booking || {};

$(document).ready(function () {
    autoSelectOnPostFailure();

    function autoSelectOnPostFailure() {
        var ticks = $('[data-booking-date-ticks]').val();

        if (ticks != undefined && ticks != null && ticks != 0) {
            var td = $('td[data-ticks="' + ticks + '"]');

            booking.selectDate(td);
        }
    }
});

booking.selectDate = function (td) {
    var date = $(td).attr('data-ticks');

    $('[data-booking-date-ticks]').val(date);

    updateUserInterface();
    validateIfNecessary();

    function updateUserInterface() {
        $('td:contains("✓")').css('background-image', 'none');
        $('td:contains("✓")').text('');
        $(td).css('background-image', 'repeating-linear-gradient(135deg, rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0) 2%)');
        $(td).text('✓');
    }

    function validateIfNecessary() {
        if ($('.field-validation-error').is(':visible')) {
            $('form').valid();
        }
    }
}

booking.updateCalendar = function (navigation) {
    var eventId = $('[data-event-id]').val();
    var ticks = $('#calendar-current-date-ticks').val();
    var params = $.param({ eventId: eventId, ticks: ticks, navigation: navigation });
    var url = $('#calendar-update-url').val() + '?' + params;

    displayProgressBar();

    $('[data-booking-date-ticks]').val(null);
    $.ajax(url).done(successHandler).fail(failureHandler);

    function displayProgressBar() {
        var height = $('#calendar-container').css('height');
        var container = '<div class="progress-container" style="height: ' + height + '; line-height: ' + height + '">{0}</div>';
        var progressBar = '<progress max="100"></progress>';

        $('#calendar-container').html(container.replace('{0}', progressBar));
    }

    function successHandler (data, textStatus, jqXHR) {
        $('#calendar-container').html(data);
    }

    function failureHandler (jqXHR, textStatus, errorThrown) {
        var message = "An error occurred!\n\n";
        var height = $('#calendar-container').css('height');
        var container = '<div class="progress-container" style="height: ' + height + '">{0}</div>';
        var paragraph = '<p>Failed to load calendar!<br />Please refresh or try again later...</p>';

        message += "Status:\t\t\t" + textStatus + "\n";
        message += "Error Details:\t" + errorThrown;

        $('#calendar-container').html(container.replace('{0}', paragraph))
        alert(message);
    }
}