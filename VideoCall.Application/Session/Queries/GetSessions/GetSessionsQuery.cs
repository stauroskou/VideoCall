using VideoCall.Application.Abstractions.Messaging;

namespace VideoCall.Application.Session.Queries.GetSessions;

public record GetSessionsQuery() : IQuery<List<Core.Entities.Session>?>;
