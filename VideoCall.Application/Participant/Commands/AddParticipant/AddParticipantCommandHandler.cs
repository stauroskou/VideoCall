using VideoCall.Application.Abstractions.Messaging;
using VideoCall.Core.Interfaces;
using VideoCall.Core.Shared;

namespace VideoCall.Application.Participant.Commands.AddParticipant;

internal sealed class AddParticipantCommandHandler(IParticipantService participantService) : ICommandHandler<AddParticipantCommand, Core.Entities.Participant>
{
    public async Task<Result<Core.Entities.Participant>> Handle(AddParticipantCommand request, CancellationToken cancellationToken)
    {
        var addedParticipant = await participantService.AddParticipantToSessionAsync(request.userId, request.sessionId);
        return addedParticipant;
    }
}