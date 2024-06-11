var connection = new signalR.HubConnectionBuilder()
    .withUrl("/commentsHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.on("ReceiveComment", (message, issueId) => {
        debugger;
        var issueComments = document.querySelector(".issue-comments");
        var newComment = document.createElement("div");
        newComment.classList.add("issue-comment");
        newComment.innerHTML = `<p>${message}</p>`
        issueComments.appendChild(newComment);
    }
);

var issueId = $("#issueId").val();
$('#addComment').click(function (event) {
    debugger;
    event.preventDefault();
    var message = $("#commentInput").val();
    $.ajax({
        url: '/App/AddComment',
        method: 'POST',
        data: {message: message, issueId: issueId},
        success: function (response) {
            console.log(response)
        },
        error: function (response) {
            alert("Comment not added: " + response.responseJSON.description)
            console.log(response)
        }
    })
    $("#commentInput").val("");
});

connection.start()
    .then(() => {
        connection.invoke("SubscribeIssue", issueId)
            .catch(err => console.error(err));
    })
    .catch(err => console.error(err));