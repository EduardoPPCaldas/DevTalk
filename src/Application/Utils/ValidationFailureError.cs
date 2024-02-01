using FluentResults;
using FluentValidation.Results;

namespace DevTalk.Application.Utils;

public class ValidationFailureError : ValidationFailure
{
    public ValidationFailureError(ValidationError error)
    {
        Error = error;
    }

    public ValidationError Error { get; set; }
}