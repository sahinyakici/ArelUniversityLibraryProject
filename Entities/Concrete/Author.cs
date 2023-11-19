using Core.Entities;
using Entities.Abstract;

namespace Entities.Concrete;

public class Author : IEntity, IDeleted
{
    public Guid AuthorId { get; set; }
    public string AuthorName { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteTime { get; set; }
}