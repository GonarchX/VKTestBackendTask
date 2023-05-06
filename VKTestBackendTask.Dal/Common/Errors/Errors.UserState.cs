using ErrorOr;

namespace VKTestBackendTask.Dal.Common.Errors;

public static partial class Errors
{
    public static class UserState
    {
        public static Error NotFound => Error.NotFound(
            code: "UserGroup.NotFound",
            description: "User state with specified parameters not found."
        );
    }
}