using VideoCall.Core.Session.Requests;
using VideoCall.Core.Shared;

namespace VideoCall.Core.Interfaces;

public interface ISessionService
{
    Task<Result<Entities.Session>> CreateSessionAsync(string name, DateTime startTime, DateTime endTime);
    Task<Result<Entities.Session>> GetSessionByIdAsync(string id);
    Task<Result<Entities.Session>> UpdateSessionAsync(string id,UpdateSessionRequest updateSession);
    Task<Result<List<Entities.Session>>> GetSessionsAsync();
    Task<Result<bool>> DeleteSessionAsync(string id);
}
