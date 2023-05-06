using ErrorOr;

namespace VKTestBackendTask.Dal.Common.Errors;

public partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Conflict(
            code: "Auth.InvalidCred",
            description: "Invalid credentials."
        );

        public static Error AlreadyExistedUser => Error.Conflict(
            code: "Auth.AlreadyExistedUser",
            description: "Trying to create user with existed login."
        );
    }
}