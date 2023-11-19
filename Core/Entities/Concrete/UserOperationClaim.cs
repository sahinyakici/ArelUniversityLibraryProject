using Entities.Abstract;

namespace Core.Entities.Concrete;

public class UserOperationClaim : IEntity, IDeleted
{
    public Guid UserOperationClaimId { get; set; }
    public Guid UserId { get; set; }
    public Guid OperationClaimId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteTime { get; set; }
}