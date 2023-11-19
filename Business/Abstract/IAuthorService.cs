using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract;

public interface IAuthorService
{
    IDataResult<List<Author>> GetAll(bool withDeleted = false);
    IDataResult<Author> GetById(Guid guid, bool withDeleted = false);
    IDataResult<Author> GetByName(string authorName);
    IResult Add(Author author);
    IResult Update(Author author);
    IResult Delete(Guid authorId, bool permanently = false);
}