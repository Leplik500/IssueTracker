let dataTable = $('#issuesTable').DataTable({
    info:false,
    serverSide: true,
    searching: false,
    paging: false,
    sorting: false,
    ajax: {
        url: "/App/GetIssues",
        method: "POST",
        data: @GetIsseus
    }
})