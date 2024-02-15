using DevTalk.Application.Users.Interfaces;
using FluentResults;

namespace DevTalk.Infrastructure.Users;

public class PhotoBucketHandler : IPhotoBucketHandler
{
    private const string FolderName = "Photos";
    private static readonly string s_pathFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FolderName);
    public async Task<Result<string>> CreateOrUpdatePhoto(Stream stream, string fileName, string? previousFileName, CancellationToken cancellationToken)
    {
        if(previousFileName is not null && File.Exists(Path.Combine(s_pathFolder, previousFileName)))
        {
            File.Delete(Path.Combine(s_pathFolder, previousFileName));
        }

        if(Directory.Exists(s_pathFolder) is false)
        {
            Directory.CreateDirectory(s_pathFolder);
        }

        var filePath = Path.Combine(s_pathFolder, Guid.NewGuid() + fileName);
        var file = File.Create(filePath);
        await stream.CopyToAsync(file, cancellationToken);
        return Path.GetFileName(filePath);
    }
}
