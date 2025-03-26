using VideoCall.Application.Abstractions.Messaging;

namespace VideoCall.Application.Participant.Commands.AddParticipant;

public record AddParticipantCommand(string sessionId, string userId) : ICommand<Core.Entities.Participant>;
