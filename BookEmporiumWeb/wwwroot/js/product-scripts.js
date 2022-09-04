$(document).ready(function () {
    
});

function validateImageInput() {
    if ($('#imageUrl').val() == "") {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Please upload a valid image!',
        })
        return false;
    } else {
        return true;
    }
}