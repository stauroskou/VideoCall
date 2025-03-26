using VideoCall.Application.Abstractions.Messaging;
using VideoCall.Core.Errors;
using VideoCall.Core.Interfaces;
using VideoCall.Core.Shared;

namespace VideoCall.Application.Session.Commands.CreateSession;

internal class CreateSessionCommandHandler(ISessionService sessionService) : ICommandHandler<CreateSessionCommand, Core.Entities.Session>
{
    public async Task<Result<Core.Entities.Session>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
       var session = await sessionService.CreateSessionAsync(request.name, request.startTime, request.endTime);
       if (session == null)
       {
           return Result.Failure<Core.Entities.Session>(DomainErrors.SessionErrors.SessionAlreadyExists);
       }

       return Result.Success(session);
    }
}
