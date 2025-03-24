using VideoCall.Core.Shared;
using MediatR;

namespace CSharpApp.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result> {}

public interface ICommand<TResponse> : IRequest<Result<TResponse>> {}