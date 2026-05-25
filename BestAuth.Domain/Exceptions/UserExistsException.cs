namespace BestAuth.Domain.Exceptions
{
    public class UserExistsException(string email) : Exception($"User {email} exists");
    public class RegistrationFailedException(IEnumerable<string> errors)
        : Exception($"Registration failed with: {string.Join(Environment.NewLine, errors)}");
    public class LoginFailedException(string email) : Exception($"Invalid email {email} or password");
    public class RefreshTokenException(string message) : Exception(message);

}
