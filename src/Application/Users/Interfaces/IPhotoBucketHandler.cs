using FluentResults;

namespace DevTalk.Application.Users.Interfaces;

public interface IPhotoBucketHandler
{
    Task<Result<string>> CreateOrUpdatePhoto(Stream stream, string fileName, string? previousFileName, CancellationToken cancellationToken);
}