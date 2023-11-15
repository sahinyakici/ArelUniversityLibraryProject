using AutoMapper;
using Business.Abstract;
using Core.Entities;
using Core.Entities.Concrete;

namespace Business.Mappers.AutoMapper.Resolvers.UserOperationClaimResolver;

public class UserOperationClaimRowIdResolver : IValueResolver<UserOperationClaimDto, UserOperationClaim, Guid>
{
    private readonly IUserOperationClaimService _userOperationClaimService;
    private readonly IOperationClaimService _operationClaimService;

    public UserOperationClaimRowIdResolver(IUserOperationClaimService userOperationClaimService,
        IOperationClaimService operationClaimService)
    {
        _userOperationClaimService = userOperationClaimService;
        _operationClaimService = operationClaimService;
    }

    public Guid Resolve(UserOperationClaimDto source, UserOperationClaim destination, Guid destMember,
        ResolutionContext context)
    {
        var operationGetResult = _operationClaimService.GetByName(source.OperationName);
        var result = _userOperationClaimService.GetAllClaimsWithUserName(source.UserName);
        if (result.Success)
        {
            Guid operationGetId = operationGetResult.Data.OperationClaimId;
            List<UserOperationClaim> userOperationClaims = result.Data;
            UserOperationClaim userOperationClaim = userOperationClaims
                .Where(operation => operation.OperationClaimId == operationGetId).SingleOrDefault();
            if (userOperationClaim == null)
            {
                return Guid.NewGuid();
            }

            return userOperationClaim.UserOperationClaimId;
        }

        throw new Exception();
    }
}