var connection = new signalR.HubConnectionBuilder()
    .withUrl("/commentsHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

// Handle incoming comment
connection.on("ReceiveComment", (message, issueId) => {
        // Add the comment to the issue comments section
        debugger;
        var issueComments = document.querySelector(".issue-comments");
        var newComment = document.createElement("div");
        newComment.classList.add("issue-comment");
        newComment.innerHTML = `<p>${message}</p>`
        issueComments.appendChild(newComment);
    }
);

// Submit the comment form
$('#addComment').click(function (event) {
    debugger;
    event.preventDefault();
    var issueId = parseInt($("#issueId").val());
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

// Start the connection
connection.start().catch((err) => {
        console.error(err.toString());
    }
);