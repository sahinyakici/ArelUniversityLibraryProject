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
        List<Author> authors = _authorDal.GetAll();
        if (authors != null)
        {
            return new SuccessDataResult<List<Author>>(authors);
        }

        return new ErrorDataResult<List<Author>>(authors, Messages.AuthorsNotFound);
    }

    public IDataResult<Author> GetById(Guid guid)
    {
        Author author = _authorDal.Get(author => author.AuthorId == guid);
        if (author != null)
        {
            return new SuccessDataResult<Author>(author);
        }

        return new ErrorDataResult<Author>(author, Messages.AuthorNotFound);
    }

    public IDataResult<Author> GetByName(string authorName)
    {
        Author author = _authorDal.Get(author => author.AuthorName.ToLower() == authorName.ToLower());
        if (author != null)
        {
            return new SuccessDataResult<Author>(author, Messages.AuthorWasFound);
        }

        return new ErrorDataResult<Author>(author, Messages.AuthorNotFound);
    }

    public IResult Add(Author author)
    {
        if (author.AuthorId == null)
        {
            author.AuthorId = Guid.NewGuid();
        }

        _authorDal.Add(author);
        return new SuccessResult(Messages.AuthorAdded);
    }
}