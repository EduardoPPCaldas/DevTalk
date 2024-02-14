using DevTalk.Application.Users.Interfaces;
using DevTalk.Application.Utils;
using DevTalk.Domain.Users;
using DevTalk.Domain.Users.Errors;
using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace DevTalk.Application.Users;

[GenerateAutomaticInterface]
public class LoginUserUseCase : ILoginUserUseCase
{
    private readonly ITokenHandler _tokenHandler;
    private readonly SignInManager<User> _signInManager;

    public LoginUserUseCase(ITokenHandler tokenHandler, SignInManager<User> signInManager)
    {
        _tokenHandler = tokenHandler;
        _signInManager = signInManager;
    }

    public async Task<Result<string>> ExecuteAsync(LoginUserRequestDTO signIn, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(signIn.Username, signIn.Password, false, false);
        if(!result.Succeeded)
        {
            return Result.Fail(new CouldNotCreateUserError());
        }

        var user = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == signIn.Username.ToUpper());

        if(user is null)
        {
            return Result.Fail(new UserNotFoundError());
        }

        return _tokenHandler.GenerateToken(user);
    }
}