using FluentValidation;

namespace DevTalk.Application.Users;

public class CreateUserRequestDTOValidator : AbstractValidator<CreateUserRequestDTO>
{
    public CreateUserRequestDTOValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.Password).Must(x => x.Any(char.IsAsciiLetter));
        RuleFor(x => x.Password).Must(x => x.Any(char.IsDigit));
        RuleFor(x => x.BirthDate).Must(x => x < DateTimeOffset.Now);
    }
}