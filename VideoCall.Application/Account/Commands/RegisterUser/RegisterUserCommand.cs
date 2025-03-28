using VideoCall.Application.Abstractions.Messaging;

namespace VideoCall.Application.Account.Commands.RegisterUser;

public record RegisterUserCommand(string userName, string password) : ICommand<string>;
