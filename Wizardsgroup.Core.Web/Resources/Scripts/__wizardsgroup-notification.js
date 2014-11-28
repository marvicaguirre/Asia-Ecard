
//=====================================
//Note: the following should be found in the master page (which is normally the _Layout.cshtml)
//=====================================
//<div class="info message"></div>
//<div class="error message"></div>
//<div class="warning message"></div>
//<div class="success message"></div>
//=====================================

var myMessages = ['info', 'warning', 'error', 'success'];

function hideAllMessages() {
    var messagesHeights = []; // this array will store height for each

    for (var i = 0; i < myMessages.length; i++) {
        messagesHeights[i] = $('.' + myMessages[i]).outerHeight(); // fill array
        $('.' + myMessages[i]).css('top', -messagesHeights[i]); //move element outside viewport
    }
}

function _notification_showJBarMessage_error(message) {
    showJBarMessage('error', message);
}

function _notification_showJBarMessage_success(message) {
    showJBarMessage('success', message);
}

function _notification_showJBarMessage_warning(message) {
    showJBarMessage('warning', message);
}

function _notification_showJBarMessage_info(message) {
    showJBarMessage('info', message);
}

function showJBarMessage(type, message) {
    hideAllMessages();

    var height = 50;

    var duration = 1500;
    var extraMessage = '';
    if (type === 'error') {
        //for debuggging only! see _medicard-notification.js.
        extraMessage = '(for debuggging only! see _medicard-notification.js); ';
        height = 800;
        duration = 5000;
    }
    var msg = "<font color=\"#FFFFFF\" size=\"4px\">" + extraMessage + message + "</font>";
    $('.' + type).empty();
    $('.' + type).html(msg);
    $('.' + type).animate({ top: "0", height: height }, 500);

    setTimeout(function () { $('.' + type).animate({ top: -$('.' + type).outerHeight() }, 500); }, duration);
}

/*****************************JBar Scripts*********************************/
$(document).ready(function () {

    // Initially, hide them all
    hideAllMessages();

    // When message is clicked, hide it
    $('.message').click(function () {
        $(this).animate({ top: -$(this).outerHeight() }, 500);
    });

});
/*****************************JBar Scripts*********************************/

function onGlobalNotification(actionResultMessage) {
    //gGlobalNotification.val("");
    //gGlobalNotification.val(actionResultMessage);
    //gGlobalNotification.trigger("change");
}
