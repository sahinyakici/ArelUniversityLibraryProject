namespace Entities.Abstract;

public interface IDeleted
{
    public bool IsDeleted { get; set; }
    public DateTime? DeleteTime { get; set; }
}