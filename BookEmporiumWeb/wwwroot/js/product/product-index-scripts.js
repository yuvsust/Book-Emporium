$(document).ready(function () {
    loadDataTable();
    $(document).on('click', '.btn-delete', deleteProduct);
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        'ajax': {
            'url': '/Admin/Product/GetAll'
        },
        'columns': [
            { 'data': 'title', 'width': '15%' },
            { 'data': 'isbn', 'width': '15%' },
            { 'data': 'price', 'width': '15%' },
            { 'data': 'author', 'width': '15%' },
            { 'data': 'category.name', 'width': '15%' },
            {
                'data': 'id',
                'render': function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/Admin/Product/Upsert?id=${data}"
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

function deleteProduct() {
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
            url: '/Admin/Product/Delete',
            data: { id: id },
            type: 'DELETE',
            success: function (data) {
                if (data.success) {
                    loadDataTable();
                    toastr.success(data.message);
                } else {
                    toastr.error(data.message);
                }
            }
        });
    })
}