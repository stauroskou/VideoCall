using VideoCall.Application.Abstractions.Messaging;

namespace VideoCall.Application.Account.Queries.GetAllUsers;

internal record GetAllUsersQuery() : IQuery<List<string>>;
