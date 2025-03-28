using VideoCall.Core.Shared;

namespace VideoCall.Core.Errors;

public static class DomainErrors
{

    public static Error GeneralError => new Error("General.Error", "An error occurred.");

    public static class ParticipantErrors
    {
        public static Error ParticipantNotFound => new Error("Participant.NotFound", "Participant not found.");
        public static Error ParticipantAlreadyExists => new Error("Participant.Exists", "Participant already exists.");
        public static Error HostCannotBeRemoved => new Error("Participant.RemoveHost", "Host cannot be removed.");
        public static Error ParticipantAlreadyInSession => new Error("Participant.AlreadyInSession", "Participant is already in session.");


    }

    public static class SessionErrors
    {
        public static Error SessionNotFound => new Error("Session.NotFound", "Session not found.");
        public static Error SessionAlreadyExists => new Error("Session.Exists", "Session already exists.");
    }

    public static class UserErrors
    {
        public static Error UserWrongUserNameOrPassword => new Error("User.WrongUserNameOrPassword", "Wrong username or password");
        public static Error UserExists => new Error("User.Exists", "User already exists.");
        public static Error UserTakenUsername => new Error("User.TakenUsername", "Username is already taken.");
        public static Error UserCheckPasswordValidations => new Error("User.TakenUsername", "Password validations failed.");
    }

}
