using Core.Entities;
using Entities.Abstract;

namespace Entities.Concrete;

public class Image : IEntity, IDeleted
{
    public Guid ImageId { get; set; }
    public Guid BookId { get; set; }
    public String ImagePath { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteTime { get; set; }
}