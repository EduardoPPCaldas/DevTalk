namespace DevTalk.Application.Users.Options;

public class AuthOptions
{
    public const string ConfigName = nameof(AuthOptions);
    public required string SymmetricSecurityKey { get; set; }
    public required int ExpireTime { get; set; }
}
