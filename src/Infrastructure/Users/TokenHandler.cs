using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevTalk.Application.Users.Interfaces;
using DevTalk.Application.Users.Options;
using DevTalk.Domain.Users;
using DevTalk.Domain.Users.Errors;
using FluentResults;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DevTalk.Infrastructure.Users;

public class TokenHandler : ITokenHandler
{
    private readonly IOptions<AuthOptions> _authOptions;

    public TokenHandler(IOptions<AuthOptions> authOptions)
    {
        _authOptions = authOptions;
    }

    public Result<string> GenerateToken(User user)
    {
        if(user.UserName is null)
        {
            return Result.Fail(new EmptyUsernameError());
        }

        List<Claim> claims = [
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("id", user.Id),
            new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString()),
            new Claim("loginTimeStamp", DateTimeOffset.Now.ToString())
        ];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Value.SymmetricSecurityKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddDays(_authOptions.Value.ExpireTime), claims: claims,
            signingCredentials: signingCredentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
