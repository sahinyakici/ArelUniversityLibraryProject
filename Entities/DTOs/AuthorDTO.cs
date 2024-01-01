using Core.Entities;

namespace Entities.DTOs;

public record AuthorDTO : IDto
{
    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; }
}