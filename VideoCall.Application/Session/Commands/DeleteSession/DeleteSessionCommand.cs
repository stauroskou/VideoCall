using VideoCall.Application.Abstractions.Messaging;

namespace VideoCall.Application.Session.Commands.DeleteSession;

public record DeleteSessionCommand(string sessionId) : ICommand<bool>;
