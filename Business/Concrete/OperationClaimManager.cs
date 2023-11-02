using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete;

public class OperationClaimManager : IOperationClaimService
{
    private IOperationClaimDal _operationClaimDal;

    public OperationClaimManager(IOperationClaimDal operationClaimDal)
    {
        _operationClaimDal = operationClaimDal;
    }

    public IDataResult<OperationClaim> GetByName(string name)
    {
        var result = _operationClaimDal.Get(oper => oper.Name == name);
        if (result != null)
        {
            return new SuccessDataResult<OperationClaim>(result, Messages.ClaimIsFound);
        }

        return new ErrorDataResult<OperationClaim>(Messages.ClaimsNotFound);
    }
}