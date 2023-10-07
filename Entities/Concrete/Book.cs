using Core.Entities;

namespace Entities.Concrete;

public class Book : IEntity
{
    public Guid BookId { get; set; }
    public Guid GenreId { get; set; }
    public Guid AuthorId { get; set; }
    public Guid OwnerId { get; set; }
    public string BookName { get; set; }
    public string Location { get; set; }
    public int PageSize { get; set; }
    public bool RentStatus { get; set; }
}