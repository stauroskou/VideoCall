using VideoCall.Application.Abstractions.Messaging;

namespace VideoCall.Application.Account.Queries.LoginUser;

internal record LoginUserQuery(string Username, string Password) : IQuery<bool>;
