using DevTalk.Application.Users.Interfaces;
using DevTalk.Application.Utils;
using DevTalk.Domain.Users;
using DevTalk.Domain.Users.Errors;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevTalk.Application.Users;

[GenerateAutomaticInterface]
public class UploadPhotoUseCase : IUploadPhotoUseCase
{
    private readonly UserManager<User> _userManager;
    private readonly IPhotoBucketHandler _photoBucketHandler;

    public UploadPhotoUseCase(UserManager<User> userManager, IPhotoBucketHandler photoBucketHandler)
    {
        _userManager = userManager;
        _photoBucketHandler = photoBucketHandler;
    }

    public async Task<Result> ExecuteAsync(Stream file, string fileName, Guid id, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id.ToString(), cancellationToken);
        if(user is null)
        {
            return Result.Fail(new UserNotFoundError());
        }

        var result = await _photoBucketHandler.CreateOrUpdatePhoto(file, fileName, user.ProfilePicture, cancellationToken);
        if(result.IsSuccess is false)
        {
            return Result.Fail(new CouldNotUploadPhotoError());
        }

        user.ProfilePicture = result.Value;
        await _userManager.UpdateAsync(user);

        return Result.Ok();
    }
}