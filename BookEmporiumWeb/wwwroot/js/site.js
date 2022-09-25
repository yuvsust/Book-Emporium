// Common JS

$(document).ready(function () {
    makeCurrentNavItemActive();
    showPageNotification();
});

function makeCurrentNavItemActive() {
    if (currentPageTitle == 'Home') {
        $('#homeNav').addClass('active');
    } else {
        $('#contentManagementNav').addClass('active');
        if (currentPageTitle == 'Category') {
            $('#categoryNav').addClass('active');
        } else if (currentPageTitle == 'Cover Type') {
            $('#coverTypeNav').addClass('active');
        } else if (currentPageTitle == 'Product') {
            $('#productNav').addClass('active');
        } else if (currentPageTitle == 'Company') {
            $('#companyNav').addClass('active');
        } 
    }
}

function showPageNotification() {
    if (successNotification) {
        toastr.success(successNotification);
    }
    if (errorNotification) {
        toastr.error(errorNotification);
    }
}