using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VideoCall.Core.Entities;
using VideoCall.Core.Interfaces;
using VideoCall.Pesistance.Persistance;


namespace VideoCall.Application.Participant;

public class ParticipantService(AppDbContext appDbContext, UserManager<User> userManager) : IParticipantService
{
    public async Task<Core.Entities.Participant?> AddParticipantToSessionAsync(string userId, string sessionId)
    {

        var session = await appDbContext.Sessions
            .Include(s=>s.Participants)
            .FirstOrDefaultAsync(s => s.Id == sessionId);

        var participant = await appDbContext.Participants.FirstOrDefaultAsync(s => s.User_Id == userId);

        participant!.SessionId = session!.Id;
        session!.Participants.Add(participant);
        await appDbContext.SaveChangesAsync();

        return participant;
    }

    public async Task CreateParticipant(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        
        var participant = new Core.Entities.Participant
        {
            Id = Guid.NewGuid().ToString(),
            Name = user!.UserName,
            Role = "Attendee",
            User_Id = user.Id
        };

        await appDbContext.Participants.AddAsync(participant);
        await appDbContext.SaveChangesAsync();
    }

    public async Task<List<Core.Entities.Participant>?> GetAllParticipantsFromSessionAsync(string sessionId)
    {
        var session = await appDbContext.Sessions
            .AsNoTracking()
            .Include(s=>s.Participants)
            .FirstOrDefaultAsync(s => s.Id == sessionId);

        if (session == null)
            return null;

        return session.Participants;
    }

    public async Task<bool> RemoveParticipantFromSessionAsync(string participantId, string sessionId)
    {
        var session = await appDbContext.Sessions
            .Include(s => s.Participants)
            .FirstOrDefaultAsync(s => s.Id == sessionId);

        if (session == null)
            return false;

        var participant = session.Participants.FirstOrDefault(s => s.Id == participantId);
        if (participant == null)
            return false;

        participant.SessionId = null;
        session.Participants.Remove(participant);
        await appDbContext.SaveChangesAsync();

        return true;
    }
}
