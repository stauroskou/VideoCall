using CSharpApp.Application.Abstractions.Messaging;
using VideoCall.Core.Shared;

namespace VideoCall.Application.Participant.Commands.CreateParticipant;

internal sealed class CreateParticipantCommandHandler : ICommandHandler<CreateParticipantCommand, Guid>
{
    public Task<Result<Guid>> Handle(CreateParticipantCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
