using VideoCall.Application.Abstractions.Messaging;
using VideoCall.Core.Interfaces;
using VideoCall.Core.Shared;

namespace VideoCall.Application.Participant.Commands.RemoveParticipant;

internal sealed class RemoveParticipantCommandHandler(IParticipantService participantService) : ICommandHandler<RemoveParticipantCommand, string>
{
    public async Task<Result<string>> Handle(RemoveParticipantCommand request, CancellationToken cancellationToken)
    {
        var result = await participantService.RemoveParticipantFromSessionAsync(request.participantId, request.sessionId);

        return result;
    }
}
