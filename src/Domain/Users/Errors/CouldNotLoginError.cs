using FluentResults;

namespace DevTalk.Domain.Users.Errors;

public class CouldNotLoginError : Error
{
    public CouldNotLoginError(): base("Could not login this user")
    {
        
    }
}
