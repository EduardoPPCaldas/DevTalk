using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace DevTalk.Domain.Users;

public class User : IdentityUser
{

    public DateTimeOffset BirthDate { get; set; }
    public string? ProfilePicture { get; set; }

    #pragma warning disable CS8618 
    public User()
    {
    }
    #pragma warning restore CS8618

    public User(DateTimeOffset birthDate, string? profilePicture)
    {
        BirthDate = birthDate;
        ProfilePicture = profilePicture;
    }
}