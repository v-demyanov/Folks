namespace Folks.ChatService.Application.Exceptions;

public class AuthenticationFailedException : Exception
{
    public AuthenticationFailedException()
        : base($"Authentication failed.")
    {
    }
}
