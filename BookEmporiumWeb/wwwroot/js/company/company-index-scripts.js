$(document).ready(function () {
    loadDataTable();
    $(document).on('click', '.btn-delete', deleteCompany);
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        'ajax': {
            'url': '/Admin/Company/GetAll'
        },
        'columns': [
            { 'data': 'name', 'width': '15%' },
            { 'data': 'streetAddress', 'width': '15%' },
            { 'data': 'city', 'width': '15%' },
            { 'data': 'state', 'width': '15%' },
            { 'data': 'postalCode', 'width': '15%' },
            { 'data': 'phoneNumber', 'width': '15%' },
            {
                'data': 'id',
                'render': function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Company/Upsert?id=${data}"
                                class="btn btn-primary"> <i class="bi bi-pencil-square"></i> Edit
                            </a>&nbsp;
                            <a class="btn btn-danger btn-delete"> <i class="bi bi-trash-fill"></i> Delete
                            </a>
                        </div>
                    `
                },
                'width': '15%'
            }
        ],
        createdRow: function (row, data, index) {
            $(row).attr('data-id', data.id);
        }
    })
}

function deleteCompany() {
    let id = $(this).closest('tr').data('id');
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        $.ajax({
            url: '/Admin/Company/Delete',
            data: { id: id },
            type: 'DELETE',
            success: function (data) {
                if (data.success) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                } else {
                    toastr.error(data.message);
                }
            }
        });
    })
}