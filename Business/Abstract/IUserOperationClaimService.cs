using Core.Entities;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;

namespace Business.Abstract;

public interface IUserOperationClaimService
{
    IResult Add(UserOperationClaimDto userOperationClaimDto);
    IResult Delete(Guid userOperationClaimId, bool permanently = false);
    IResult DeleteAllClaims(string userName, bool permanently = false);
    DataResult<List<UserOperationClaim>> GetAllClaimsWithUserName(string userName, bool withDeleted = false);
}