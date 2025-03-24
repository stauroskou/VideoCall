using CSharpApp.Application.Abstractions.Messaging;

namespace VideoCall.Application.Participant.Commands.CreateParticipant;

public record CreateParticipantCommand(string Name) : ICommand<Guid>;
