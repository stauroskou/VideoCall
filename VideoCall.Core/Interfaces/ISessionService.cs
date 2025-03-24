using VideoCall.Core.Entities;

namespace VideoCall.Core.Interfaces;

internal interface ISessionService
{
    Task<Session> CreateSessionAsync();
    Task<Session> GetSessionAsync();
    Task<Session> UpdateSessionAsync();
    Task<bool> DeleteSessionAsync();
}
