using DevTalk.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevTalk.Infrastructure.Users;

public class UserDatabase(DbContextOptions<UserDatabase> opt) : IdentityDbContext<User>(opt)
{
    public const string UserDatabaseConnectionString = nameof(UserDatabaseConnectionString);
}