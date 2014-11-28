function getExpiryDays(franchiseExpiryId, comboBoxId, hiddenId) {
    
    if (franchiseExpiryId != '') {
        var url = _director_fixUrl(_g_common_Virtualpath + "/Common/FranchiseExpiry/GetExpiryDate?franchiseExpiryId=" + franchiseExpiryId);
        
        $.ajax({
            url: url,
            type: 'POST',
            dataType: 'text',
            success: function (data) {
                var result = JSON.parse(data);
                $('#' + hiddenId).val(result);
                $('#' + comboBoxId).val(result);
                $('#' + comboBoxId).focus();
            },
            error: function (xhr, status, err) {
                _director_showWarning(err);
            }
        });
    } else {
        $('#' + comboBoxId).val('');
    }
}

function isSelectedDateLessThanExpectedDate(selDate,expDate) {
    var selectedDate = new Date(selDate);
    var expectedDate = new Date(expDate);

    if (selectedDate < expectedDate) {
        return true;
    } else {
        return false;
    }
}

function loadForm(status) {
    if (status == "Approve") {
        $('.isApproved').show();
        $('.isDisapproved').hide();
    }
    else if (status == "Disapprove") {
        $('.isApproved').hide();
        $('.isDisapproved').show();
    } else {
        $('.isApproved').hide();
        $('.isDisapproved').hide();
    }
}