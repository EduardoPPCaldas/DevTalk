using System.Security.Cryptography;
using DevTalk.Application.Utils;
using DevTalk.Domain.Users;
using DevTalk.Domain.Users.Errors;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace DevTalk.Application.Users;

[GenerateAutomaticInterface]
public class CreateUserUseCase(IValidator<CreateUserRequestDTO> validator, UserManager<User> userManager) : ICreateUserUseCase
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IValidator<CreateUserRequestDTO> _validator = validator;

    public async Task<Result<CreateUserResponseDTO>> ExecuteAsync(CreateUserRequestDTO dto, CancellationToken cancellationToken)
    {
        var validation = _validator.Validate(dto);
        if (!validation.IsValid)
        {
            var validationResult = new Result();
            foreach (var error in validation.Errors)
            {
                if (error is ValidationFailureError validationError)
                {
                    validationResult.WithError(validationError.Error);
                }
            }
            return validationResult;
        }

        var user = new User
        {
            Email = dto.Email,
            BirthDate = dto.BirthDate,
            UserName = dto.UserName
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            return Result.Fail(new CouldNotCreateUserError());
        }

        return new CreateUserResponseDTO(dto.Email);
    }
}