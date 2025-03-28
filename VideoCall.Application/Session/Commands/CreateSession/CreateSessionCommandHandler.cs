using VideoCall.Application.Abstractions.Messaging;
using VideoCall.Core.Interfaces;
using VideoCall.Core.Shared;

namespace VideoCall.Application.Session.Commands.CreateSession;

internal class CreateSessionCommandHandler(ISessionService sessionService, IParticipantService participantService) : ICommandHandler<CreateSessionCommand, Core.Entities.Session>
{
    public async Task<Result<Core.Entities.Session>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await sessionService.CreateSessionAsync(request.name, request.startTime, request.endTime);

        if (session.IsFailure)
            return session;
            

        await participantService.AddParticipantToSessionAsync(request.hostId, session.Value!.Id, isHost: true);

        return session;
    }
}
