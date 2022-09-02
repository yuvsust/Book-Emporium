// Common JS

$(document).ready(function () {
    makeCurrentNavItemActive();

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
        } else if (currentPageTitle == 'Category') {
            $('#productNav').addClass('active');
        }
    }
}