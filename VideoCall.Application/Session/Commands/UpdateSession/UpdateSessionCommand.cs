using VideoCall.Application.Abstractions.Messaging;

namespace VideoCall.Application.Session.Commands.UpdateSession;

public record UpdateSessionCommand(string id,string? name, DateTime? startTime, DateTime? endTime) : ICommand<Core.Entities.Session>;
