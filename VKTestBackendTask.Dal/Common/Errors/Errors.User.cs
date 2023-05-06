using ErrorOr;

namespace VKTestBackendTask.Dal.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error NotFound => Error.NotFound(
            code: "User.NotFound",
            description: "User with specified parameters not found."
        );

        public static Error AdminAlreadyExist => Error.NotFound(
            code: "User.AdminAlreadyExist",
            description: "User with admin role already exist."
        );
    }
}