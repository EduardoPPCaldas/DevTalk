using FluentResults;

namespace DevTalk.Domain.Users.Errors;

public class CouldNotCreateUserError : Error
{
    public CouldNotCreateUserError() : base("Could not create user")
    {
    }
}