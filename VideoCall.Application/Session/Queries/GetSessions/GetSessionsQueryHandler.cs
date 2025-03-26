using VideoCall.Application.Abstractions.Messaging;
using VideoCall.Core.Interfaces;
using VideoCall.Core.Shared;

namespace VideoCall.Application.Session.Queries.GetSessions;

internal class GetSessionsQueryHandler(ISessionService sessionService) : IQueryHandler<GetSessionsQuery, List<Core.Entities.Session>?>
{
    public async Task<Result<List<Core.Entities.Session>?>> Handle(GetSessionsQuery request, CancellationToken cancellationToken)
    {
        var sessions = await sessionService.GetSessionsAsync();
        return Result.Success(sessions);
    }
}

