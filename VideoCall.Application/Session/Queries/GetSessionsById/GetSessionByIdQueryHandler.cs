using VideoCall.Application.Abstractions.Messaging;
using VideoCall.Core.Interfaces;
using VideoCall.Core.Shared;

namespace VideoCall.Application.Session.Queries.GetSessionsById;

internal class GetSessionByIdQueryHandler(ISessionService sessionService) : IQueryHandler<GetSessionByIdQuery, Core.Entities.Session>
{
    public async Task<Result<Core.Entities.Session>> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
    {
        var session = await sessionService.GetSessionByIdAsync(request.sessionId);

        return session;
    }
}
