using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
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

        return new ErrorDataResult<List<Author>>(Messages.AuthorsNotFound);
    }

    public IDataResult<Author> GetById(Guid guid)
    {
        Author author = _authorDal.Get(author => author.AuthorId == guid);
        if (author != null)
        {
            return new SuccessDataResult<Author>(author);
        }

        return new ErrorDataResult<Author>(Messages.AuthorNotFound);
    }

    public IDataResult<Author> GetByName(string authorName)
    {
        Author author = _authorDal.Get(author => author.AuthorName.ToLower() == authorName.ToLower());
        if (author != null)
        {
            return new SuccessDataResult<Author>(author, Messages.AuthorWasFound);
        }

        return new ErrorDataResult<Author>(Messages.AuthorNotFound);
    }

    [SecuredOperation("author.add,admin,editor,user")]
    [ValidationAspect(typeof(AuthorValidator))]
    public IResult Add(Author author)
    {
        if (author.AuthorId == null)
        {
            author.AuthorId = Guid.NewGuid();
        }

        _authorDal.Add(author);
        return new SuccessResult(Messages.AuthorAdded);
    }

    [SecuredOperation("author.update,admin,editor")]
    [ValidationAspect(typeof(AuthorValidator))]
    public IResult Update(Author author)
    {
        _authorDal.Update(author);
        return new SuccessResult(Messages.AuthorUpdated);
    }
}