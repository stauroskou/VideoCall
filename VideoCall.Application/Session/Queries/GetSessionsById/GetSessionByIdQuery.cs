using VideoCall.Application.Abstractions.Messaging;

namespace VideoCall.Application.Session.Queries.GetSessionsById;

public record GetSessionByIdQuery(string sessionId) : IQuery<Core.Entities.Session>;
