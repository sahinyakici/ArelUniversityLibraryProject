using AutoMapper;
using Business.Abstract;
using Core.Entities;
using Core.Entities.Concrete;

namespace Business.Mappers.AutoMapper.Resolvers.UserOperationClaimResolver;

public class UserOperationClaimIdResolver : IValueResolver<UserOperationClaimDto, UserOperationClaim, Guid>
{
    private IOperationClaimService _operationClaimService;

    public UserOperationClaimIdResolver(IOperationClaimService operationClaimService)
    {
        _operationClaimService = operationClaimService;
    }

    public Guid Resolve(UserOperationClaimDto source, UserOperationClaim destination, Guid destMember,
        ResolutionContext context)
    {
        var result = _operationClaimService.GetByName(source.OperationName);
        if (result.Success)
        {
            return result.Data.OperationClaimId;
        }

        throw new Exception(result.Message);
    }
}