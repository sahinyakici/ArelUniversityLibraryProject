using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class UserManager : IUserService
{
    private IUserDal _userDal;

    public UserManager(IUserDal userDal)
    {
        _userDal = userDal;
    }

    public IDataResult<List<User>> GetAll()
    {
        List<User> users = _userDal.GetAll();
        if (users != null)
        {
            return new SuccessDataResult<List<User>>(users, Messages.UsersListed);
        }

        return new ErrorDataResult<List<User>>(users, Messages.UsersNotFound);
    }

    public IDataResult<User> GetById(Guid userId)
    {
        User user = _userDal.Get(g => g.UserId == userId);
        if (user == null)
        {
            return new ErrorDataResult<User>(user, Messages.UserNotFound);
        }

        return new SuccessDataResult<User>(user, Messages.UserFound);
    }

    public IDataResult<User> GetByName(string name)
    {
        User user = _userDal.Get(g => g.UserName == name);
        if (user == null)
        {
            return new ErrorDataResult<User>(user, Messages.UserNotFound);
        }

        return new SuccessDataResult<User>(user, Messages.UserFound);
    }

    public IDataResult<User> GetByMail(string mail)
    {
        User user = _userDal.Get(g => g.UserMail == mail);
        if (user == null)
        {
            return new ErrorDataResult<User>(user, Messages.UserNotFound);
        }

        return new SuccessDataResult<User>(user, Messages.UserFound);
    }

    public IResult Add(User user)
    {
        if (user.UserId == null)
        {
            user.UserId = Guid.NewGuid();
        }

        _userDal.Add(user);
        return new SuccessResult(Messages.UserCreated);
    }

    public IResult Update(User user)
    {
        _userDal.Update(user);
        return new SuccessResult(Messages.UserUpdated);
    }
}