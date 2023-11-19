using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;

namespace Business.Abstract;

public interface IUserService
{
    IDataResult<List<User>> GetAll(bool withDeleted = false);
    IDataResult<User> GetById(Guid userId, bool withDeleted = false);
    IDataResult<User> GetByMail(string mail, bool withDeleted = false);
    IDataResult<User> GetByUserName(string userName, bool withDeleted = false);
    IResult Add(User user);
    IResult Update(User user);
    IResult Delete(Guid userId, bool permanently = false);
    IDataResult<List<OperationClaim>> GetClaims(User user, bool withDeleted = false);
}