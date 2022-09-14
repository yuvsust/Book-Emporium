$(document).ready(function () {
    loadTinyEditor();
});

function loadTinyEditor() {
    tinymce.init({
        selector: 'textarea',
        plugins: 'casechange export formatpainter image editimage linkchecker autolink lists checklist media mediaembed pageembed permanentpen powerpaste table advtable tableofcontents tinycomments tinymcespellchecker',
        toolbar_mode: 'floating',
        tinycomments_mode: 'embedded',
        tinycomments_author: 'Author name',
    });
}


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