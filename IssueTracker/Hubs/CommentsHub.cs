using Microsoft.AspNetCore.SignalR;

namespace IssueTracker.Hubs;

public class CommentsHub : Hub {

    public async Task SendComment(String message, Int64 issueId)
    {
        await Clients.All.SendAsync("ReceiveComment", message, issueId);
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}