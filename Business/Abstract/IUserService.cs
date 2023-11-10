using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;

namespace Business.Abstract;

public interface IUserService
{
    IDataResult<List<User>> GetAll();
    IDataResult<User> GetById(Guid userId);
    IDataResult<User> GetByFirstName(string firstName);
    IDataResult<User> GetByLastName(string lastName);
    IDataResult<User> GetByMail(string mail);
    IDataResult<User> GetByUserName(string userName);
    IResult Add(User user);
    IResult Update(User user);
    IDataResult<List<OperationClaim>> GetClaims(User user);
}