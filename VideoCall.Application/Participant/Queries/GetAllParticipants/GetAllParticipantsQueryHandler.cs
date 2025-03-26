using VideoCall.Application.Abstractions.Messaging;
using VideoCall.Core.Interfaces;
using VideoCall.Core.Shared;

namespace VideoCall.Application.Participant.Queries.GetAllParticipants;

internal class GetAllParticipantsQueryHandler(IParticipantService participantService) : IQueryHandler<GetAllParticipantsQuery, List<Core.Entities.Participant>?>
{
    
    public async Task<Result<List<Core.Entities.Participant>?>> Handle(GetAllParticipantsQuery request, CancellationToken cancellationToken)
    {
        var participants = await participantService.GetAllParticipantsFromSessionAsync(request.sessionId);
        return Result.Success(participants);
    }
}

