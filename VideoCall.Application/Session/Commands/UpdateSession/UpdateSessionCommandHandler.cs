using VideoCall.Application.Abstractions.Messaging;
using VideoCall.Core.Errors;
using VideoCall.Core.Interfaces;
using VideoCall.Core.Session.Requests;
using VideoCall.Core.Shared;

namespace VideoCall.Application.Session.Commands.UpdateSession;

internal class UpdateSessionCommandHandler(ISessionService sessionService) : ICommandHandler<UpdateSessionCommand, Core.Entities.Session>
{
    public async Task<Result<Core.Entities.Session>> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
    {
        var req = new UpdateSessionRequest
        { 
            name = request.name,
            startTime = request.startTime, 
            endTime = request.endTime 
        };

        var session =  await sessionService.UpdateSessionAsync(request.id, req);

        return session;

    }
}
