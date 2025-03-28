using VideoCall.Application.Abstractions.Messaging;

namespace VideoCall.Application.Participant.Commands.RemoveParticipant;

public record class RemoveParticipantCommand(string participantId, string sessionId) : ICommand<string>;
