
using DevTalk.Application.Users;
using DevTalk.Application.Utils;
using DevTalk.Domain.Users.Errors;
using DevTalk.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DevTalk.WebApp.Endpoints.Users;

public class UserEndpoint : IEndpointDefinition
{
    public void AddEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("users");
        group.MapPost("register", Register);
        group.MapPost("login", Login);
    }

    public static async Task<IResult> Register(
        [FromServices] ICreateUserUseCase useCase,
        [FromBody] CreateUserRequestDTO dto,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(dto, cancellationToken);
        if(result.IsFailed)
        {
            return result.MapToApiErrorResponse();
        }
        return Results.Created("", result.Value);
    }

    public static async Task<IResult> Login(
        [FromServices] ILoginUserUseCase useCase,
        [FromBody] LoginUserRequestDTO dto,
        CancellationToken cancellationToken)
    {
        var result = await useCase.ExecuteAsync(dto, cancellationToken);
        if(result.IsFailed)
        {
            return result.MapToApiErrorResponse();
        }

        return Results.Ok(result.Value);
    }
}