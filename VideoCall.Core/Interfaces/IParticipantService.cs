using VideoCall.Core.Entities;

namespace VideoCall.Core.Interfaces;

public interface IParticipantService
{
    Task<Participant?> AddParticipantToSessionAsync(string userId, string sessionId);
    Task<bool> RemoveParticipantFromSessionAsync(string participantId, string sessionId);
    Task<List<Participant>?> GetAllParticipantsFromSessionAsync(string sessionId);
    Task CreateParticipant(string userId);

}
