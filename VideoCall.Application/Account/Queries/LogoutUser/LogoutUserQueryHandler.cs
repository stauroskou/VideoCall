using VideoCall.Application.Abstractions.Messaging;
using VideoCall.Core.Shared;

namespace VideoCall.Application.Account.Queries.LogoutUser;

internal class LogoutUserQueryHandler : IQueryHandler<LogoutUserQuery, bool>
{
    public Task<Result<bool>> Handle(LogoutUserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
