using VideoCall.Application.Abstractions.Messaging;
using VideoCall.Core.Shared;

namespace VideoCall.Application.Account.Queries.LoginUser;

internal class LoginUserQueryHandler : IQueryHandler<LoginUserQuery, bool>
{
    public Task<Result<bool>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
