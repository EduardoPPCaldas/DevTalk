using DevTalk.Application.Utils;
using DevTalk.Domain.Users.Errors;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace DevTalk.WebApp.Extensions;

public static class ResultExtensions
{
    public static IResult MapToApiErrorResponse<T>(this Result<T> result)
    {
        if(result.HasError<CouldNotCreateUserError>())
            return Results.BadRequest();
        
        if(result.HasError<ValidationError>(out var validationErrors))
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Validation Error",
                Detail = "There is some problem with the input of your message",
                Extensions = validationErrors.Select(x => x.ProblemDetails.Extensions).Aggregate((acc, x) => 
                {
                    acc = acc.Concat(x.ToList()).ToDictionary();
                    return acc;
                })
            };
            return Results.BadRequest(problemDetails);
        }
        
        if(result.HasError<EmptyUsernameError>(out var emptyUsernameErrors))
        {
            return Results.BadRequest(new ProblemDetails
            {
                Title = "Empty Username in user",
                Detail = "The username cannot be null or empty",
            });
        }

        if(result.HasError<CouldNotLoginError>())
        {
            return Results.BadRequest();
        }

        if(result.HasError<UserNotFoundError>(out var userNotFoundErrors))
        {
            return Results.NotFound();
        }

        return Results.BadRequest();
    }

    public static IResult MapToApiErrorResponse(this Result result)
    {
        if(result.HasError<CouldNotCreateUserError>())
            return Results.BadRequest();
        
        if(result.HasError<ValidationError>(out var validationErrors))
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Validation Error",
                Detail = "There is some problem with the input of your message",
                Extensions = validationErrors.Select(x => x.ProblemDetails.Extensions).Aggregate((acc, x) => 
                {
                    acc = acc.Concat(x.ToList()).ToDictionary();
                    return acc;
                })
            };
            return Results.BadRequest(problemDetails);
        }
        
        if(result.HasError<EmptyUsernameError>(out var emptyUsernameErrors))
        {
            return Results.BadRequest(new ProblemDetails
            {
                Title = "Empty Username in user",
                Detail = "The username cannot be null or empty",
            });
        }

        if(result.HasError<CouldNotLoginError>())
        {
            return Results.BadRequest();
        }

        if(result.HasError<UserNotFoundError>(out var userNotFoundErrors))
        {
            return Results.NotFound();
        }

        return Results.BadRequest();
    }
}