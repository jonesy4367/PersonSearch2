$(document).ready(function() {
    $("#searchButton").on("click", function() {

        function convertFormToJson(form) {
            var formArray = form.serializeArray();
            var json = {};

            $.each(formArray, function() {
                json[this.name] = this.value || "";
            });

            return json;
        };

        $.ajax({
            url: "PersonSearch/SearchPeople",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(convertFormToJson($("#personSearchForm"))),
            success: function(result) {
                console.log(result);
            }
        });
    });
})