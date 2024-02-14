using DevTalk.Application.Users;
using DevTalk.Application.Users.Options;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DevTalk.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        services.AddScoped<IValidator<CreateUserRequestDTO>, CreateUserRequestDTOValidator>();
        services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();

        services
            .AddOptions<AuthOptions>()
            .BindConfiguration(AuthOptions.ConfigName)
            .ValidateOnStart();

        return services;
    }
}
