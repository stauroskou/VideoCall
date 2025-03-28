using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace VideoCall.Api.Hubs;


public class SessionHub() : Hub
{
    public async Task NotifyParticipantJoinned(string sessionId)
    {
        await Clients.All.SendAsync("ParticipantJoinned", $"{Context.ConnectionId} has joined the session {sessionId}.");
    }

    public async Task NotifyParticipantLeft(string sessionId)
    {
        await Clients.All.SendAsync("ParticipantLeft", $"{Context.ConnectionId} has left the session {sessionId}.");
    }

    public async Task NotifySessionCreated(string userId)
    {
        await Clients.All.SendAsync("SessionCreated", userId);
    }

    public async Task NotifySessionDeleted(string sessionId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, sessionId);
        await Clients.Group(sessionId).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has left the group {sessionId}.");
    }

    public async Task NotifySessionUpdated(string sessionId, string message)
    {
        await Clients.Group(sessionId).SendAsync("ReceiveMessage", $"{Context.ConnectionId}: {message}");
    }
}
