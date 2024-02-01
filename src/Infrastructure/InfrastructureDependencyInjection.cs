using DevTalk.Domain.Users;
using DevTalk.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevTalk.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<UserDatabase>((sp, opt) =>
            opt.UseNpgsql(sp.GetRequiredService<IConfiguration>().GetConnectionString(UserDatabase.UserDatabaseConnectionString), 
            x => x.MigrationsAssembly(typeof(UserDatabase).Assembly.FullName)));

        return services;
    }
}
