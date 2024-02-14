using FluentResults;

namespace DevTalk.Domain.Users.Errors;

public class UserNotFoundError : Error
{
    public UserNotFoundError() : base("User could not be found")
    {
        
    }
}
