using ErrorOr;

namespace VKTestBackendTask.Dal.Common.Errors;

public partial class Errors
{
    public static class Auth
    {
        public static Error InvalidCredentials => Error.Conflict(
            code: "Auth.InvalidCredentials",
            description: "Invalid credentials."
        );

        public static Error AlreadyExistedUser => Error.Conflict(
            code: "Auth.AlreadyExistedUser",
            description: "Trying to create user with existing login."
        );
    }
}