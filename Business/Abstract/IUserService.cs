using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract;

public interface IUserService
{
    IDataResult<List<User>> GetAll();
    IDataResult<User> GetById(Guid userId);
    IDataResult<User> GetByName(string name);
    IDataResult<User> GetByMail(string mail);
    IResult Add(User user);
    IResult Update(User user);
}