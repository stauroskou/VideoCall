﻿using VideoCall.Application.Abstractions.Messaging;
using VideoCall.Core.Interfaces;
using VideoCall.Core.Shared;

namespace VideoCall.Application.Session.Commands.DeleteSession;

internal class DeleteSessionCommandHandler(ISessionService sessionService) : ICommandHandler<DeleteSessionCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
    {
        var deleted = await sessionService.DeleteSessionAsync(request.sessionId);

        return deleted;
    }
}
