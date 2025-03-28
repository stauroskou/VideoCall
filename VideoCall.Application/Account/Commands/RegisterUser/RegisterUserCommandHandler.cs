using VideoCall.Application.Abstractions.Messaging;
using VideoCall.Core.Shared;

namespace VideoCall.Application.Account.Commands.RegisterUser;

internal class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, string>
{
    public Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
