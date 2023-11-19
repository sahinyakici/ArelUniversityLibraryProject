namespace Core.Entities.Concrete;

public class OperationClaim : IEntity
{
    public Guid OperationClaimId { get; set; }
    public string Name { get; set; }
}