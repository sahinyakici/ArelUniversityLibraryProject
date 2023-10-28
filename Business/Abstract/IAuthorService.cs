using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract;

public interface IAuthorService
{
    IDataResult<List<Author>> GetAll();
    IDataResult<Author> GetById(Guid guid);
    IDataResult<Author> GetByName(string authorName);
    IResult Add(Author author);
}