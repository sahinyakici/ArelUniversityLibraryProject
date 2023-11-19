using Core.Entities;
using Entities.Abstract;

namespace Entities.Concrete;

public class Genre : IEntity, IDeleted
{
    public Guid GenreId { get; set; }
    public string GenreName { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteTime { get; set; }
}