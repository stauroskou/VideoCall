using VideoCall.Application.Abstractions.Messaging;

namespace VideoCall.Application.Session.Commands.CreateSession;

public record CreateSessionCommand(string name, string hostId, DateTime startTime, DateTime endTime) : ICommand<Core.Entities.Session>;