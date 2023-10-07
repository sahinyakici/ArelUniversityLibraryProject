using Core.Entities;

namespace Entities.Concrete;

public class Rental : IEntity
{
    public Guid RentalId { get; set; }
    public DateTime RentalStart { get; set; }
    public DateTime? RentalStop { get; set; }
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public float? RentalPrice { get; set; }
}