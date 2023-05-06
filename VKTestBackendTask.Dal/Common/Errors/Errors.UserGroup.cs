using ErrorOr;

namespace VKTestBackendTask.Dal.Common.Errors;

public static partial class Errors
{
    public static class UserGroup
    {
        public static Error NotFound => Error.NotFound(
            code: "UserGroup.NotFound",
            description: "User group with specified parameters not found."
        );
    }
}