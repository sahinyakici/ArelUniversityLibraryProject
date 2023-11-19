using Core.Entities;
using Entities.Abstract;

namespace Entities.Concrete;

public class Rental : IEntity, IDeleted
{
    public Guid RentalId { get; set; }
    public DateTime RentalStart { get; set; }
    public DateTime? RentalStop { get; set; }
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public float? RentalPrice { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteTime { get; set; }
}