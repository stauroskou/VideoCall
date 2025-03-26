using Microsoft.EntityFrameworkCore;
using VideoCall.Application.Abstractions.Messaging;
using VideoCall.Core.Entities;
using VideoCall.Core.Errors;
using VideoCall.Core.Interfaces;
using VideoCall.Core.Shared;
using VideoCall.Pesistance.Persistance;

namespace VideoCall.Application.Participant.Commands.AddParticipant;

internal sealed class AddParticipantCommandHandler(AppDbContext appDbContext, IParticipantService participantService) : ICommandHandler<AddParticipantCommand, Core.Entities.Participant>
{
    public async Task<Result<Core.Entities.Participant>> Handle(AddParticipantCommand request, CancellationToken cancellationToken)

    {

        var session = await appDbContext.Sessions.Include(s => s.Participants).FirstOrDefaultAsync(s => s.Id == request.sessionId);
        if (session == null) 
        {
            return Result.Failure<Core.Entities.Participant>(DomainErrors.SessionErrors.SessionNotFound);
        }

        var participant = await appDbContext.Participants.FirstOrDefaultAsync(s => s.User_Id == request.userId);
        if (participant == null)
        {
            await participantService.CreateParticipant(request.userId);
        }

        var addedParticipant = await participantService.AddParticipantToSessionAsync(request.userId, request.sessionId);
        return Result.Success(addedParticipant!);
    }
}