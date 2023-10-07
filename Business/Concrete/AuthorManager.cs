using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class AuthorManager : IAuthorService
{
    private IAuthorDal _authorDal;

    public AuthorManager(IAuthorDal authorDal)
    {
        _authorDal = authorDal;
    }

    public IDataResult<List<Author>> GetAll()
    {
        return new SuccessDataResult<List<Author>>(_authorDal.GetAll());
    }

    public IDataResult<Author> GetById(Guid guid)
    {
        return new SuccessDataResult<Author>(_authorDal.Get(author => author.AuthorId == guid));
    }

    public IResult Add(Author author)
    {
        author.AuthorId = Guid.NewGuid();
        _authorDal.Add(author);
        return new SuccessResult(Messages.AuthorAdded);
    }
}