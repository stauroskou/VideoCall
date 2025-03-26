using VideoCall.Application.Abstractions.Messaging;

namespace VideoCall.Application.Participant.Queries.GetAllParticipants;

public sealed record GetAllParticipantsQuery(string sessionId) : IQuery<List<Core.Entities.Participant>>;
