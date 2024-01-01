namespace Core.Entities;

public record UserOperationClaimDto : IDto
{
    public string OperationName { get; set; }
    public string UserName { get; set; }
}