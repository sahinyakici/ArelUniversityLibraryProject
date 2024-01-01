using Core.Entities;

namespace Entities.DTOs;

public record UserGetDto(string UserId, string UserName) : IDto;