namespace Core.Entities;

public class UserOperationClaimDto : IDto
{
    public string OperationName { get; set; }
    public string UserName { get; set; }
}