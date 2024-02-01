using DevTalk.Application.Users;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DevTalk.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        services.AddScoped<IValidator<CreateUserRequestDTO>, CreateUserRequestDTOValidator>();

        return services;
    }
}
