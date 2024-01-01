using Core.Entities;

namespace Entities.DTOs;

public record GenreDTO : IDto
{
    public Guid GenreId { get; set; }
    public string GenreName { get; set; }
    public int BookCount { get; set; }
}