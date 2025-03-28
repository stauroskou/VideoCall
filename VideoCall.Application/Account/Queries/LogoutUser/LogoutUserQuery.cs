using VideoCall.Application.Abstractions.Messaging;

namespace VideoCall.Application.Account.Queries.LogoutUser;

internal record LogoutUserQuery() : IQuery<bool>;
