using Core.Entities;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;

namespace Business.Abstract;

public interface IUserOperationClaimService
{
    IResult Add(UserOperationClaimDto userOperationClaimDto);
    IResult Delete(UserOperationClaimDto userOperationClaimDto);
    IResult DeleteAllClaims(string userName);
    DataResult<List<UserOperationClaim>> GetAllClaimsWithUserName(string userName);
}