using FluentResults;

namespace DevTalk.Domain.Users.Errors;

public class CouldNotUploadPhotoError : Error
{
    public CouldNotUploadPhotoError() : base("Could not upload photo")
    {
        
    }
}