using System.ComponentModel.DataAnnotations;

namespace DevTalk.Application.Users;

public sealed record CreateUserRequestDTO(
    [property: Required] string Email,
    [property: Required] string Password,
    [property: Required] string UserName,
    [property: Required] DateTimeOffset BirthDate);