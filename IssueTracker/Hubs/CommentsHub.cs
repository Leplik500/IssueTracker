using Microsoft.AspNetCore.SignalR;

namespace IssueTracker.Hubs;

public class CommentsHub : Hub {

    public async Task SubscribeIssue(String issueId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, issueId);
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