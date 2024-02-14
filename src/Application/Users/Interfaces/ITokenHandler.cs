using DevTalk.Domain.Users;
using FluentResults;

namespace DevTalk.Application.Users.Interfaces;

public interface ITokenHandler
{
    Result<string> GenerateToken(User user);
}
