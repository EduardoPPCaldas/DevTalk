using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace DevTalk.Application.Utils;

public class ValidationError : Error
{
    public ValidationError(ProblemDetails problemDetails) : base(problemDetails.Title)
    {
        ProblemDetails = problemDetails;
    }

    public ProblemDetails ProblemDetails { get; set; }
}