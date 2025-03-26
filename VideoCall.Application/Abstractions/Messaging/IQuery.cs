using VideoCall.Core.Shared;
using MediatR;

namespace VideoCall.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> {}