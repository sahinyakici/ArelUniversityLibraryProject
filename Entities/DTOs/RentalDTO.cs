using Core.Entities;

namespace Entities.DTOs;

public record RentalDTO : IDto
{
    public Guid RentalId { get; set; }
    public Guid BookId { get; set; }
    public DateTime RentalStart { get; set; }
    public DateTime? RentalStop { get; set; }
    public string UserName { get; set; }
    public string OwnerName { get; set; }
    public string BookName { get; set; }
    public float? RentalPrice { get; set; }
}