using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Transaction;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;

namespace Business.Concrete;

public class UserManager : IUserService
{
    private IUserDal _userDal;

    public UserManager(IUserDal userDal)
    {
        _userDal = userDal;
    }

    [SecuredOperation("user,admin,editor")]
    public IDataResult<List<User>> GetAll()
    {
        List<User> users = _userDal.GetAll();
        if (users != null)
        {
            return new SuccessDataResult<List<User>>(users, Messages.UsersListed);
        }

        return new ErrorDataResult<List<User>>(Messages.UsersNotFound);
    }

    public IDataResult<User> GetById(Guid userId)
    {
        User user = _userDal.Get(g => g.UserId == userId);
        if (user == null)
        {
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        return new SuccessDataResult<User>(user, Messages.UserFound);
    }

    public IDataResult<User> GetByMail(string mail)
    {
        User user = _userDal.Get(g => g.Email == mail);
        if (user == null)
        {
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        return new SuccessDataResult<User>(user, Messages.UserFound);
    }

    public IDataResult<User> GetByUserName(string userName)
    {
        User user = _userDal.Get(g => g.UserName == userName);
        if (user == null)
        {
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        return new SuccessDataResult<User>(user, Messages.UserFound);
    }

    [TransactionScopeAspect]
    public IResult Add(User user)
    {
        if (user.UserId == null)
        {
            user.UserId = Guid.NewGuid();
        }

        _userDal.Add(user);
        return new SuccessResult(Messages.UserCreated);
    }

    [TransactionScopeAspect]
    [SecuredOperation("user.update,admin,editor,user")]
    public IResult Update(User user)
    {
        _userDal.Update(user);
        return new SuccessResult(Messages.UserUpdated);
    }

    public IDataResult<List<OperationClaim>> GetClaims(User user)
    {
        var result = _userDal.GetClaims(user);
        if (result.Count != null)
        {
            return new SuccessDataResult<List<OperationClaim>>(result);
        }

        return new ErrorDataResult<List<OperationClaim>>(Messages.ClaimsNotFound);
    }
}