using Core.Entities;

namespace Entities.Concrete;

public class Author : IEntity
{
    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; }
}