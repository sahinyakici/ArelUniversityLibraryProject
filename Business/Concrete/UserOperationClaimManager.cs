using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
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

    public IResult Add(UserOperationClaimDto userOperationClaimDto)
    {
        UserOperationClaim userOperationClaim = _mapper.Map<UserOperationClaim>(userOperationClaimDto);
        _userOperationClaimDal.Add(userOperationClaim);
        return new SuccessResult(Messages.UserOperationClaimAdded);
    }

    [SecuredOperation("useroperation.delete,admin,editor")]
    public IResult Delete(UserOperationClaimDto userOperationClaimDto)
    {
        UserOperationClaim userOperationClaim = _mapper.Map<UserOperationClaim>(userOperationClaimDto);
        _userOperationClaimDal.Delete(userOperationClaim);
        return new SuccessResult(Messages.UserOperationClaimDeleted);
    }

    [SecuredOperation("useroperation.delete,admin,editor")]
    public IResult DeleteAllClaims(string userName)
    {
        User user = _userService.GetByUserName(userName).Data;
        List<UserOperationClaim> userOperationClaims =
            _userOperationClaimDal.GetAll(userOperation => userOperation.UserId == user.UserId);
        foreach (UserOperationClaim userOperationClaim in userOperationClaims)
        {
            _userOperationClaimDal.Delete(userOperationClaim);
        }

        return new SuccessResult(Messages.UserOperationsDeleted);
    }

    [SecuredOperation("useroperation.get,admin,editor")]
    public DataResult<List<UserOperationClaim>> GetAllClaimsWithUserName(string userName)
    {
        User user = _userService.GetByUserName(userName).Data;
        List<UserOperationClaim> userOperationClaims =
            _userOperationClaimDal.GetAll(operations => operations.UserId == user.UserId);
        return new SuccessDataResult<List<UserOperationClaim>>(userOperationClaims, Messages.UserOperationsFinded);
    }
}