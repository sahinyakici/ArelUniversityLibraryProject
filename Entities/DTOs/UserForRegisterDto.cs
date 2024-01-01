using Core.Entities;

namespace Entities.DTOs;

public record UserForRegisterDto : IDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordCheck { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
}