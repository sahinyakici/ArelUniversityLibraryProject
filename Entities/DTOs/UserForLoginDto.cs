using Core.Entities;

namespace Entities.DTOs;

public record UserForLoginDto : IDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}