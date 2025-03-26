using VideoCall.Core.Session.Requests;

namespace VideoCall.Core.Interfaces;

public interface ISessionService
{
    Task<Entities.Session?> CreateSessionAsync(string name, DateTime startTime, DateTime endTime);
    Task<Entities.Session?> GetSessionByIdAsync(string id);
    Task<Entities.Session?> UpdateSessionAsync(string id,UpdateSessionRequest updateSession);
    Task<List<Entities.Session>?> GetSessionsAsync();
    Task<bool> DeleteSessionAsync(string id);
}
