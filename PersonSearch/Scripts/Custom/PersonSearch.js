﻿$(document).ready(function() {
    $("#searchButton").on("click", function() {

        function convertFormToJson(form) {
            var formArray = form.serializeArray();
            var json = {};

            $.each(formArray, function() {
                json[this.name] = this.value || "";
            });

            return json;
        };

        function generateTableContents(data) {
            var html = "";

            $.each(data, function(personIndex, person) {
                var interestsHtml = "";

                var interestsLength = person.Interests.length;

                if (person.Interests) {
                    interestsHtml += "<table class='no-border'>";

                    $.each(person.Interests, function (interestIndex, interest) {
                        var tdClass;

                        if (interestIndex === interestsLength - 1) {
                            tdClass = "no-border";
                        } else {
                            tdClass = "no-border interest_td";
                        }

                        interestsHtml +=
                            "<tr>" +
                            "<td class='" + tdClass + "'>" + interest + "</td>" +
                            "</tr>";
                    });

                    interestsHtml += "</table>";
                }

                html +=
                    "<tr>" +
                    "<td>" + person.FullName + "</td>" +
                    "<td>" + person.Address + "</td>" +
                    "<td>" + interestsHtml + "</td>" +
                    "</tr>";
            });

            return html;
        };

        $.ajax({
            url: "PersonSearch/SearchPeople",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(convertFormToJson($("#personSearchForm"))),
            success: function(result) {
                $("#personTable").html(generateTableContents(result));
            }
        });
    });
})