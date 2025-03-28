using Microsoft.EntityFrameworkCore;
using VideoCall.Core.Errors;
using VideoCall.Core.Interfaces;
using VideoCall.Core.Session.Requests;
using VideoCall.Core.Shared;
using VideoCall.Pesistance.Persistance;

namespace VideoCall.Application.Session;

public class SessionService(AppDbContext appDbContext, ISessionNotifier notifier) : ISessionService
{
    public async Task<Result<Core.Entities.Session>> CreateSessionAsync(string name, DateTime startTime, DateTime endTime)
    {
        var sessionExists = await appDbContext.Sessions.FirstOrDefaultAsync(s => s.Name == name);

        if (sessionExists != null)
            return Result.Failure<Core.Entities.Session>(DomainErrors.SessionErrors.SessionAlreadyExists);

        var session = new Core.Entities.Session()
        {
            Id = Guid.NewGuid().ToString(),
            HasEndedByHost = false,
            Name = name,
            EndTime = endTime,
            StartTime = startTime,
        };

        

        await appDbContext.Sessions.AddAsync(session);
        appDbContext.SaveChanges();

        await notifier.NotifySessionCreated(new Core.Dto.SessionDto { Id = session.Id, Name = session.Name, StartTime = session.StartTime, EndTime = session.EndTime, Duration =  session.Duration });
        return Result.Success(session);
    }

    public async Task<Result<bool>> DeleteSessionAsync(string id)
    {
        var session = await appDbContext.Sessions.FirstOrDefaultAsync(s => s.Id == id);

        if (session == null)
            return Result.Failure<bool>(DomainErrors.SessionErrors.SessionNotFound);

        appDbContext.Sessions.Remove(session);
        appDbContext.SaveChanges();

        await notifier.NotifySessionDeleted(session.Id);
        return Result.Success(true);
    }

    public async Task<Result<Core.Entities.Session>> GetSessionByIdAsync(string id)
    {
        var session = await appDbContext.Sessions
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (session == null)
            return Result.Failure<Core.Entities.Session>(DomainErrors.SessionErrors.SessionNotFound);

        return Result.Success(session);
    }

    public async Task<Result<List<Core.Entities.Session>>> GetSessionsAsync()
    {
        var sessions = await appDbContext.Sessions
            .AsNoTracking()
            .Include(s=>s.Participants) 
            .ToListAsync();

        return Result.Success(sessions);
    }

    public async Task<Result<Core.Entities.Session>> UpdateSessionAsync(string id,UpdateSessionRequest updateSession)
    {
        var existingSession = await appDbContext.Sessions.FirstOrDefaultAsync(s => s.Id == id);

        if (existingSession == null)
            return Result.Failure<Core.Entities.Session>(DomainErrors.SessionErrors.SessionNotFound);

        existingSession.Name = updateSession.name ?? existingSession.Name;
        existingSession.StartTime = updateSession.startTime ?? existingSession.StartTime;
        existingSession.EndTime = updateSession.endTime ?? existingSession.EndTime;
        appDbContext.SaveChanges();

        return Result.Success(existingSession);
    }
}
