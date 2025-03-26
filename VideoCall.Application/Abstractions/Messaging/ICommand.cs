using VideoCall.Core.Shared;
using MediatR;

namespace VideoCall.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result> {}

public interface ICommand<TResponse> : IRequest<Result<TResponse>> {}