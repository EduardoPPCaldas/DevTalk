using FluentResults;

namespace DevTalk.Domain.Users.Errors;

public class EmptyUsernameError : Error
{
   public EmptyUsernameError() : base("This user has a null username")
   {
    
   } 
}
