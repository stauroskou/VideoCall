using VideoCall.Core.Entities;

namespace VideoCall.Core.Interfaces;

public interface IParticipantService
{
    Task<Participant> AddParticipantToSessionAsync();
    Task<bool> RemoveParticipantFromSessionAsync();
    Task<Participant> GetAllParticipantsFromSessionAsync();
}
