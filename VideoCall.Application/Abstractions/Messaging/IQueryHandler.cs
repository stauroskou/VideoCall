using VideoCall.Core.Shared;
using MediatR;

namespace CSharpApp.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery: IQuery<TResponse> {}
