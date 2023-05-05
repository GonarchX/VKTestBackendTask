using ErrorOr;

namespace VKTestBackendTask.Dal.Common.Errors;

public static partial class Errors
{
    public static partial class User
    {
        public static Error NotFound => Error.NotFound(
            code: "User.NotFound",
            description: "User with specified not found."
        );
    }
}