using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;

namespace Business.Abstract;

public interface IOperationClaimService
{
    IDataResult<OperationClaim> GetByName(string name);
}