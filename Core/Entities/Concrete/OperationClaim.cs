namespace Core.Entities.Concrete;

public class OperationClaim : IEntity
{
    public int Id { get; set; }
    public Guid OperationClaimId { get; set; }
    public string Name { get; set; }
}