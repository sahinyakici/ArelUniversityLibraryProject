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
    public IDataResult<List<User>> GetAll(bool withDeleted = false)
    {
        List<User> users = _userDal.GetAll(user => !user.IsDeleted || withDeleted);
        if (users != null)
        {
            return new SuccessDataResult<List<User>>(users, Messages.UsersListed);
        }

        return new ErrorDataResult<List<User>>(Messages.UsersNotFound);
    }

    public IDataResult<User> GetById(Guid userId, bool withDeleted = false)
    {
        User user = _userDal.Get(g => g.UserId == userId && (withDeleted || !g.IsDeleted));
        if (user == null)
        {
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        return new SuccessDataResult<User>(user, Messages.UserFound);
    }

    public IDataResult<User> GetByMail(string mail, bool withDeleted = false)
    {
        User user = _userDal.Get(g => g.Email == mail && (withDeleted || !g.IsDeleted));
        if (user == null)
        {
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        return new SuccessDataResult<User>(user, Messages.UserFound);
    }

    public IDataResult<User> GetByUserName(string userName, bool withDeleted = false)
    {
        User user = _userDal.Get(g => g.UserName == userName && (withDeleted || !g.IsDeleted));
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

    public IResult Delete(Guid userId, bool permanently = false)
    {
        User user = _userDal.Get(user => user.UserId == userId);
        if (user != null)
        {
            if (!permanently)
            {
                user.IsDeleted = true;
                user.DeleteTime = DateTime.UtcNow;
                _userDal.Update(user);
                return new SuccessResult(Messages.UserDeleted);
            }

            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeletedPermanently);
        }

        return new ErrorResult(Messages.UserNotFound);
    }

    public IDataResult<List<OperationClaim>> GetClaims(User user, bool withDeleted = false)
    {
        List<OperationClaim> results = _userDal.GetClaims(user, withDeleted);
        if (results.Count != null)
        {
            return new SuccessDataResult<List<OperationClaim>>(results);
        }

        return new ErrorDataResult<List<OperationClaim>>(Messages.ClaimsNotFound);
    }
}