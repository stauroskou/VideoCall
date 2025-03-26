using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using VideoCall.Core.Interfaces;
using VideoCall.Core.Session.Requests;
using VideoCall.Pesistance.Persistance;

namespace VideoCall.Application.Session;

public class SessionService(AppDbContext appDbContext) : ISessionService
{
    public async Task<Core.Entities.Session?> CreateSessionAsync(string name, DateTime startTime, DateTime endTime)
    {
        var sessionExists = await appDbContext.Sessions.FirstOrDefaultAsync(s => s.Name == name);
        if (sessionExists != null)
            return null;

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
        return session;
    }

    public async Task<bool> DeleteSessionAsync(string id)
    {
        var session = await appDbContext.Sessions.FirstOrDefaultAsync(s => s.Id == id);
        if (session == null)
            return false;

        appDbContext.Sessions.Remove(session);
        appDbContext.SaveChanges();
        return true;
    }

    public async Task<Core.Entities.Session?> GetSessionByIdAsync(string id)
    {
        var session = await appDbContext.Sessions.Include(s => s.Participants).FirstOrDefaultAsync(s => s.Id == id);
        if (session == null)
            return null;

        return session;
    }

    public async Task<List<Core.Entities.Session>?> GetSessionsAsync()
    {
        var sessions = await appDbContext.Sessions.Include(s=>s.Participants).AsNoTracking().ToListAsync();
        return sessions;
    }

    public async Task<Core.Entities.Session?> UpdateSessionAsync(string id,UpdateSessionRequest updateSession)
    {
        var existingSession = await appDbContext.Sessions.FirstOrDefaultAsync(s => s.Id == id);
        if (existingSession == null)
            return null;

        existingSession.Name = updateSession.name ?? existingSession.Name;
        existingSession.StartTime = updateSession.startTime ?? existingSession.StartTime;
        existingSession.EndTime = updateSession.endTime ?? existingSession.EndTime;
        appDbContext.SaveChanges();

        return existingSession;
    }
}
