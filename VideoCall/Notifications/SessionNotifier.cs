using Microsoft.AspNetCore.SignalR;
using VideoCall.Api.Hubs;
using VideoCall.Core.Dto;
using VideoCall.Core.Entities;
using VideoCall.Core.Interfaces;

namespace VideoCall.Api.Notifications;

public class SessionNotifier(IHubContext<SessionHub> hub) : ISessionNotifier
{
    public Task NotifyParticipantAdded(string userId, string sessionId)
    {
        throw new NotImplementedException();
    }

    public Task NotifyParticipantLeft(string userId, string sessionId)
    {
        throw new NotImplementedException();
    }

    public async Task NotifySessionCreated(SessionDto session)
    {
        await hub.Clients.All.SendAsync("SessionCreated", session);
    }

    public Task NotifySessionDeleted(string userId)
    {
        throw new NotImplementedException();
    }
}
