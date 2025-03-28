using VideoCall.Core.Dto;

namespace VideoCall.Core.Interfaces;

public interface ISessionNotifier
{
    Task NotifySessionCreated(SessionDto session);
    Task NotifySessionDeleted(string sessionId);
    Task NotifyParticipantAdded(string userId, string sessionId);
    Task NotifyParticipantLeft(string userId, string sessionId);
}
