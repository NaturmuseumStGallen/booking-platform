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

var admin = admin || {};

$(document).ready(function () {
    attachSafetyHandlers();
    attachDatePickers();
    attachColorPreview();
    attachAutoColor();
    attachSlideHandlers();

    function attachSafetyHandlers() {
        var elements = $('[data-safety-message]');

        $.each(elements, function (index, element) {
            var onclick = $(element).onclick;

            $(element).click(function (event) {
                safetyHandler(element, onclick, event);
            });
        });
    }

    function attachDatePickers() {
        $("[data-date-picker]").datepicker({ dateFormat: 'dd.mm.yy' });
    }

    function attachColorPreview() {
        var inputs = $('.color-input input[type=range]');

        $.each(inputs, function (index, input) {
            $(input).change(function () {
                colorPreview();
            });
        });

        colorPreview();
    }

    function attachAutoColor() {
        var elements = $('[data-auto-color]');
        var settings = { attributes: true, attributeFilter: ['style'] };

        var observer = new MutationObserver(function (mutations) {
            $.each(mutations, function (index, record) {
                observer.disconnect();
                autoColor(record.target);
                observer.observe(record.target, settings);
            });
        });

        $.each(elements, function (index, element) {
            observer.observe(element, settings);
            autoColor(element);
        });
    }

    function attachSlideHandlers() {
        var triggers = $('[data-slide-trigger]');

        $.each(triggers, function (index, trigger) {
            var originalText = $(trigger).text();
            var open = true;

            $(trigger).append(' ▴');
            $(trigger).click(function () {
                var id = $(trigger).attr('data-slide-trigger');

                open = !open;

                $('[data-slide="' + id + '"]').slideToggle();
                $(trigger).html(originalText + (open ? ' ▴' : ' ▾'));
            });
        });
    }

    function safetyHandler(element, onclick, event) {
        var message = $(element).attr('data-safety-message');

        if (window.confirm(message)) {
            onclick.call(element, event);
            
            return true;
        }

        event.preventDefault();

        return false;
    }

    function colorPreview() {
        var r = $('[data-color-red]').val();
        var g = $('[data-color-green]').val();
        var b = $('[data-color-blue]').val();

        $('[data-color-red]').attr('title', r);
        $('[data-color-green]').attr('title', g);
        $('[data-color-blue]').attr('title', b);

        $('[data-color-preview]').css('background-color', 'rgb(' + r + ', ' + g + ', ' + b + ')');
    }

    function autoColor(element) {
        var rgb = $(element).css('background-color');
        var colors = rgb.substring(rgb.indexOf('(') + 1, rgb.lastIndexOf(')')).split(/,\s*/);
        var r = colors[0];
        var g = colors[1];
        var b = colors[2];

        // See: https://www.w3.org/TR/AERT#color-contrast
        var brightness = Math.sqrt((r * r * 0.299) + (g * g * 0.587) + (b * b * 0.114));

        $(element).css('color', brightness < 150 ? 'white' : 'black');
    }

    $("#js-add-booking-time").click(function () {
        var timeToAdd = $("#js-booking-time-to-add").val();

        var timeFormatRegex = new RegExp("^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");

        if (!timeFormatRegex.test(timeToAdd))
            return;

        var tdBookingTime = "<td><input readonly type='text' name='BookingTimes' value='" + timeToAdd + "' /></td>";
        var tdDeleteBookingTime = "<td class='align-right deletable-row'>X</td>";

        var newRowContent = "<tr>" + tdBookingTime + tdDeleteBookingTime + "</tr>";
        $("#override-bookingtimes tbody").append(newRowContent);
    });

    $("#override-bookingtimes tbody").on("click", "td.deletable-row", function () {
        $(this).closest('tr').remove();
    });

    $("#sortable-events").sortable();
    $("#sortable-events").disableSelection();

    $("#js-submitEventDisplayOrder").click(function () {
        var eventIds = $("#sortable-events").sortable("toArray");

        var url = $('#display-order-update-url').val()

        $.post(url, $.param({ data: eventIds }, true)).fail(failureHandler);

        function failureHandler(jqXHR, textStatus, errorThrown) {
            var message = "An error occurred!\n\n";

            message += "Status:\t\t\t" + textStatus + "\n";
            message += "Error Details:\t" + errorThrown;
  
            alert(message);
        }
    });
});




