
using VideoCall.Core.Shared;

namespace VideoCall.Core.Errors;

public static class DomainErrors
{
    public static class ParticipantErrors
    {
        public static Error ParticipantNotFound => new Error("Participant.NotFound", "Participant not found.");
        public static Error ParticipantAlreadyExists => new Error("Participant.Exists", "Participant already exists.");
    }

    public static class SessionErrors
    {
        public static Error SessionNotFound => new Error("Session.NotFound", "Session not found.");
        public static Error SessionAlreadyExists => new Error("Session.Exists", "Session already exists.");
    }

}
