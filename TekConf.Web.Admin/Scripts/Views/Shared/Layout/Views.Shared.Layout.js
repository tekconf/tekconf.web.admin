$(function () {
    $('.validateForm').areYouSure({ 'message': 'You have not saved your changes!' });
});

function wireupSignalR(formId) {
    if (formId != null) {
        var formEditingHub = $.connection.formEditingHub;
        $.connection.hub.logging = true;

        function init() {
            formEditingHub.server.joinGroup(formId);
        }

        formEditingHub.client.disableForm = function (formDisabledMessage) {
            $("#" + formDisabledMessage.FormName + " :input").attr("disabled", true);
            $('#refreshPage').show();
        };

        $.connection.hub.start(function () {
            init();
        }).done(function () { });
    }
}