using Core.Entities;
using Entities.Abstract;

namespace Entities.Concrete;

public class Book : IEntity, IDeleted
{
    public Guid BookId { get; set; }
    public Guid GenreId { get; set; }
    public Guid AuthorId { get; set; }
    public Guid OwnerId { get; set; }
    public string BookName { get; set; }
    public string Location { get; set; }
    public int PageSize { get; set; }
    public bool RentStatus { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteTime { get; set; }
}