using VideoCall.Application.Abstractions.Messaging;
using VideoCall.Core.Errors;
using VideoCall.Core.Interfaces;
using VideoCall.Core.Shared;

namespace VideoCall.Application.Participant.Commands.RemoveParticipant;

internal sealed class RemoveParticipantCommandHandler(IParticipantService participantService) : ICommandHandler<RemoveParticipantCommand>
{
    public async Task<Result> Handle(RemoveParticipantCommand request, CancellationToken cancellationToken)
    {
        var result = await participantService.RemoveParticipantFromSessionAsync(request.participantId, request.sessionId);
        if (!result)
        {
            return Result.Failure(DomainErrors.ParticipantErrors.ParticipantNotFound);
        }

        return Result.Success();
    }
}
