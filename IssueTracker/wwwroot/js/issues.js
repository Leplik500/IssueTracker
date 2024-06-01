let dataTable = $('#issuesTable').DataTable({
    info: false,
    serverSide: true,
    searching: false,
    paging: false,
    sorting: false,
    ajax: {
        url: "/App/GetIssues",
        method: "POST",
        data: null
    },
    columns: [
        {data: 'title'},
        {data: 'priority'},
        {data: 'status'},
        {
            data: null,
            sortable: false,
            render: function (data, type) {
                return '<button> Details </button>'
            }
        }
    ],
    createdRow: function (nRow, data) {
        var handlerNew = function () {
            
        }
        
        for (var i = 0; i < dataTable.columns().header().length - 1; i++) {
            $('td', nRow).eq(i).css('cursor', 'pointer');
            $('td', nRow).eq(i).on('click', null);
        }
        $('td button', nRow).on('click', null);
    }
})