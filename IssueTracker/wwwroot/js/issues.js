$('#newIssue').on('click', function () {
    window.location.href = '/App/CreateIssue';
});


//     document.getElementById('myModal').style.display = 'block';
//     document.querySelector('.modal-overlay').style.display = 'block';

function closeModal() {
    const modal = $('#modal');
    modal.removeClass('active');
    $('.modal-overlay').removeClass('active');
}

function openModal(parameters) {
    const id = parameters.data;
    const url = parameters.url;
    const modal = $('#modal');
    if (id === undefined || url === undefined) {
        alert("Invalid parameters");
        return;
    }
    $.ajax({
        type: 'GET',
        url: url,
        data: {"id": id},
        success: function (response) {
            modal.find(".modal-content").html(response);
            modal.addClass('active');
            $('.modal-overlay').addClass('active');
            $('.close-modal').click(function () {
                closeModal();
            });
            $(document).on('click', function (event) {
                if ($('div.container').has(event.target).length === 0) {
                    closeModal();
                }
            });
        },
        failure: function () {
            modal.modal('hide')
        },
        error: function (response) {
            alert(response.responseText);
        }
    })
}

let dataTable = $('#issuesTable').DataTable({
    info: false,
    serverSide: true,
    searching: false,
    paging: false,
    sorting: false,
    ajax: {
        url: "/App/GetIssues",
        method: "POST",
        data: {}
    },
    columns: [
        {data: 'title'},
        {data: 'priority'},
        {data: 'status'},
    ],
    createdRow: function (nRow, data) {
        var handlerDetails = function () {
            debugger;
            openModal({url: '/App/GetIssue', data: data.id})
        }

        for (var i = 0; i < dataTable.columns().header().length; i++) {
            $('td', nRow).eq(i).css('cursor', 'pointer');
            $('td', nRow).eq(i).on('click', handlerDetails);
        }
    }
})