using Core.Entities;

namespace Entities.Concrete;

public class Genre : IEntity
{
    public Guid GenreId { get; set; }
    public string GenreName { get; set; }
}