using Core.Entities;

namespace Entities.DTOs;

public class BookDTO : IDto
{
    public Guid BookId { get; set; }
    public string BookName { get; set; }
    public string GenreName { get; set; }
    public string AuthorName { get; set; }
    public string OwnerName { get; set; }
    public string Location { get; set; }
    public int PageSize { get; set; }
    public bool RentStatus { get; set; }
    public float Money { get; set; }
}