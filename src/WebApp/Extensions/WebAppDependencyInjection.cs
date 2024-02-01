using System.Runtime.CompilerServices;
using DevTalk.Domain.Users;
using DevTalk.Infrastructure.Users;
using DevTalk.WebApp.Endpoints;
using DevTalk.WebApp.Endpoints.Users;

namespace DevTalk.WebApp.Extensions;

public static class WebAppDependencyInjection
{
    public static IServiceCollection AddWebAppServices(this IServiceCollection services)
    {
        services.AddAuthorization();
        services
            .AddIdentityApiEndpoints<User>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<UserDatabase>();
        services.AddTransient<IEndpointDefinition, UserEndpoint>();

        return services;
    }

    public static WebApplication AddEndpointDefinitions(this WebApplication app)
    {
        var endpoints = app.Services.GetServices<IEndpointDefinition>();

        foreach(var endpoint in endpoints)
        {
            if(endpoint is null)
            {
                continue;
            }

            endpoint.AddEndpoints(app);
        }

        return app;
    }
}