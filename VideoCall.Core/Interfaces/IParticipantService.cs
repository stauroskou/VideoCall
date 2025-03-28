using VideoCall.Core.Entities;
using VideoCall.Core.Shared;

namespace VideoCall.Core.Interfaces;

public interface IParticipantService
{
    Task<Result<Participant>> AddParticipantToSessionAsync(string userId, string sessionId, bool isHost = false);
    Task<Result<string>> RemoveParticipantFromSessionAsync(string participantId, string sessionId);
    Task<Result<List<Participant>>> GetAllParticipantsFromSessionAsync(string sessionId);
    //Task<Result> CreateParticipant(string userId, bool isHost = false);

}
