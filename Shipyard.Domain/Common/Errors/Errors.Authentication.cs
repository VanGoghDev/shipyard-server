using ErrorOr;

namespace Shipyard.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidPassword => Error.Validation(
            code: "Auth.InvalidPassword",
            description: "Invalid email or password.");
    }
}