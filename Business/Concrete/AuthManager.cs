using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Entities;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Business.Concrete;

public class AuthManager : IAuthService
{
    private readonly IMapper _mapper;
    private ITokenHelper _tokenHelper;
    private IUserOperationClaimService _userOperationClaimService;
    private IUserService _userService;

    public AuthManager(IUserService userService, ITokenHelper tokenHelper, IMapper mapper,
        IUserOperationClaimService userOperationClaimService)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
        _mapper = mapper;
        _userOperationClaimService = userOperationClaimService;
    }

    [ValidationAspect(typeof(UserRegisterValidator))]
    [TransactionScopeAspect]
    public IDataResult<User> Register(UserForRegisterDto userForRegisterDto)
    {
        User user = _mapper.Map<User>(userForRegisterDto);
        _userService.Add(user);
        IResult businessRuleResult = BusinessRules.Run(AddClaimsForUser(user.UserName));
        if (businessRuleResult != null)
        {
            return new ErrorDataResult<User>(businessRuleResult.Message);
        }

        return new SuccessDataResult<User>(user, Messages.UserRegistered);
    }

    [ValidationAspect(typeof(UserLoginValidator))]
    public IDataResult<User> Login(UserForLoginDto userForLoginDto)
    {
        var result = _userService.GetByUserName(userForLoginDto.UserName);
        if (result.Success)
        {
            var userToCheck = result.Data;

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash,
                    userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        return new ErrorDataResult<User>(Messages.UserNotFound);
    }

    public IResult UserExists(string email, string userName)
    {
        if (_userService.GetByMail(email).Data != null)
        {
            return new ErrorResult(Messages.UserAlreadyExists);
        }

        if (_userService.GetByUserName(userName).Data != null)
        {
            return new ErrorResult(Messages.UserNameAlreadyExists);
        }

        return new SuccessResult();
    }

    public IDataResult<AccessToken> CreateAccessToken(User user, bool withDeleted = false)
    {
        var result = _userService.GetClaims(user, withDeleted);
        if (result.Success)
        {
            List<OperationClaim> claims = result.Data;
            AccessToken accessToken = _tokenHelper.CreateToken(user, claims);
            if (accessToken != null)
            {
                return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
            }

            return new ErrorDataResult<AccessToken>(Messages.AccessTokenNotCreated);
        }

        return new ErrorDataResult<AccessToken>(Messages.ClaimsNotFound);
    }

    private IResult AddClaimsForUser(string userName)
    {
        UserOperationClaimDto userOperationClaimDto = new UserOperationClaimDto
            { UserName = userName, OperationName = "user" };
        return _userOperationClaimService.Add(userOperationClaimDto);
    }
}