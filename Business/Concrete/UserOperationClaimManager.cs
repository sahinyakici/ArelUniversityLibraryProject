using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Entities;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete;

public class UserOperationClaimManager : IUserOperationClaimService
{
    private readonly IMapper _mapper;
    private IUserOperationClaimDal _userOperationClaimDal;
    private IUserService _userService;

    public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IMapper mapper,
        IUserService userService)
    {
        _userOperationClaimDal = userOperationClaimDal;
        _mapper = mapper;
        _userService = userService;
    }

    [CacheRemoveAspect("IUserOperationClaimService.Get")]
    [TransactionScopeAspect]
    [PerformanceAspect(2)]
    public IResult Add(UserOperationClaimDto userOperationClaimDto)
    {
        UserOperationClaim userOperationClaim = _mapper.Map<UserOperationClaim>(userOperationClaimDto);
        _userOperationClaimDal.Add(userOperationClaim);
        return new SuccessResult(Messages.UserOperationClaimAdded);
    }

    [CacheRemoveAspect("IUserOperationClaimService.Get")]
    [TransactionScopeAspect]
    [PerformanceAspect(2)]
    [SecuredOperation("useroperation.delete,admin,editor")]
    public IResult Delete(Guid userOperationClaimId, bool permanently = false)
    {
        UserOperationClaim userOperationClaim =
            _userOperationClaimDal.Get(userope => userope.UserOperationClaimId == userOperationClaimId);
        if (!permanently)
        {
            userOperationClaim.IsDeleted = true;
            userOperationClaim.DeleteTime = DateTime.UtcNow;
            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(Messages.UserOperationsDeleted);
        }

        _userOperationClaimDal.Delete(userOperationClaim);
        return new SuccessResult(Messages.UserOperationClaimDeletedPermanently);
    }

    [CacheRemoveAspect("IUserOperationClaimService.Get")]
    [TransactionScopeAspect]
    [PerformanceAspect(2)]
    [SecuredOperation("useroperation.delete,admin,editor")]
    public IResult DeleteAllClaims(string userName, bool permanently = false)
    {
        User user = _userService.GetByUserName(userName).Data;
        List<UserOperationClaim> userOperationClaims =
            _userOperationClaimDal.GetAll(userOperation => userOperation.UserId == user.UserId);
        foreach (UserOperationClaim userOperationClaim in userOperationClaims)
        {
            if (!permanently)
            {
                userOperationClaim.IsDeleted = true;
                userOperationClaim.DeleteTime = DateTime.UtcNow;
                _userOperationClaimDal.Update(userOperationClaim);
                continue;
            }

            _userOperationClaimDal.Delete(userOperationClaim);
        }

        return new SuccessResult(Messages.UserOperationsDeleted);
    }

    [CacheAspect]
    [PerformanceAspect(2)]
    public DataResult<List<UserOperationClaim>> GetAllClaimsWithUserName(string userName, bool withDeleted = false)
    {
        var userResult = _userService.GetByUserName(userName);
        if (userResult.Success)
        {
            User user = userResult.Data;
            List<UserOperationClaim> userOperationClaims =
                _userOperationClaimDal.GetAll(operations =>
                    operations.UserId == user.UserId && (withDeleted || !operations.IsDeleted));
            return new SuccessDataResult<List<UserOperationClaim>>(userOperationClaims, Messages.UserOperationsFinded);
        }

        return new ErrorDataResult<List<UserOperationClaim>>(userResult.Message);
    }
}