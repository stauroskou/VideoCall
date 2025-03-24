using VideoCall.Core.Shared;
using MediatR;

namespace CSharpApp.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> {}