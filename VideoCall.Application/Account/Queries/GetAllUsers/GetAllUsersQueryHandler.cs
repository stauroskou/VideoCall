using VideoCall.Application.Abstractions.Messaging;
using VideoCall.Core.Shared;

namespace VideoCall.Application.Account.Queries.GetAllUsers;

internal class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, List<string>>
{
    public Task<Result<List<string>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
