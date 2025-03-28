using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VideoCall.Core.Entities;
using VideoCall.Core.Errors;
using VideoCall.Core.Interfaces;
using VideoCall.Core.Shared;
using VideoCall.Pesistance.Persistance;


namespace VideoCall.Application.Participant;

public class ParticipantService(AppDbContext appDbContext, UserManager<User> userManager, ISessionNotifier notifier) : IParticipantService
{
    public async Task<Result<Core.Entities.Participant>> AddParticipantToSessionAsync(string userId, string sessionId, bool isHost = false)
    {
        var session = await appDbContext.Sessions
            .Include(s=>s.Participants)
            .FirstOrDefaultAsync(s => s.Id == sessionId);

        if (session == null)
            return Result.Failure<Core.Entities.Participant>(DomainErrors.SessionErrors.SessionNotFound);

        var participantExistInSession = session.Participants.Exists(s => s.User_Id == userId);
        
        if (participantExistInSession)
            return Result.Failure<Core.Entities.Participant>(DomainErrors.ParticipantErrors.ParticipantAlreadyInSession);

        var participant = await CreateParticipant(userId, sessionId, isHost);

        if (participant.IsFailure)
            return Result.Failure<Core.Entities.Participant>(DomainErrors.GeneralError);

        session.Participants.Add(participant.Value);
        await appDbContext.SaveChangesAsync();

        await notifier.NotifyParticipantAdded(userId, sessionId);
        return Result.Success(participant.Value);
    }

    private async Task<Result<Core.Entities.Participant>> CreateParticipant(string userId,string sessionId ,bool isHost = false)
    {
        var user = await userManager.FindByIdAsync(userId);

        var participant = new Core.Entities.Participant
        {
            Id = Guid.NewGuid().ToString(),
            Name = user!.UserName,
            Role = isHost? "Host" : "Attendee",
            User_Id = user.Id,
            SessionId = sessionId
        };

        await appDbContext.Participants.AddAsync(participant);
        await appDbContext.SaveChangesAsync();

        return Result.Success(participant);
    }

    public async Task<Result<List<Core.Entities.Participant>>> GetAllParticipantsFromSessionAsync(string sessionId)
    {
        var session = await appDbContext.Sessions
            .AsNoTracking()
            .Include(s=>s.Participants)
            .FirstOrDefaultAsync(s => s.Id == sessionId);

        if (session == null)
            return Result.Failure<List<Core.Entities.Participant>>(DomainErrors.SessionErrors.SessionNotFound);
        
        return Result.Success(session.Participants);
    }

    public async Task<Result<string>> RemoveParticipantFromSessionAsync(string participantId, string sessionId)
    {
        var session = await appDbContext.Sessions
            .Include(s => s.Participants)
            .FirstOrDefaultAsync(s => s.Id == sessionId);

        if (session == null)
            return Result.Failure<string>(DomainErrors.SessionErrors.SessionNotFound);

        var participant = session.Participants.FirstOrDefault(s => s.Id == participantId);

        if (participant == null)
            return Result.Failure<string>(DomainErrors.ParticipantErrors.ParticipantNotFound);

        participant.SessionId = null;
        session.Participants.Remove(participant);
        await appDbContext.SaveChangesAsync();

        await notifier.NotifyParticipantLeft(participantId, sessionId);
        return Result.Success(participant.Id);
    }
}
