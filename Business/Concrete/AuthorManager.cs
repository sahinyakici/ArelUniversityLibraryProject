using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
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

    [CacheAspect]
    [PerformanceAspect(2)]
    public IDataResult<List<Author>> GetAll(bool withDeleted = false)
    {
        List<Author> authors = _authorDal.GetAll(author => withDeleted || !author.IsDeleted);
        if (authors != null)
        {
            return new SuccessDataResult<List<Author>>(authors);
        }

        return new ErrorDataResult<List<Author>>(Messages.AuthorsNotFound);
    }

    [CacheAspect]
    [PerformanceAspect(2)]
    public IDataResult<Author> GetById(Guid guid, bool withDeleted = false)
    {
        Author author = _authorDal.Get(author => author.AuthorId == guid && (withDeleted || !author.IsDeleted));
        if (author != null)
        {
            return new SuccessDataResult<Author>(author);
        }

        return new ErrorDataResult<Author>(Messages.AuthorNotFound);
    }

    [CacheAspect]
    [PerformanceAspect(2)]
    public IDataResult<Author> GetByName(string authorName)
    {
        Author author = _authorDal.Get(author => author.AuthorName.ToLower() == authorName.ToLower());
        if (author != null)
        {
            return new SuccessDataResult<Author>(author, Messages.AuthorWasFound);
        }

        return new ErrorDataResult<Author>(Messages.AuthorNotFound);
    }

    [CacheRemoveAspect("IAuthorService.Get")]
    [PerformanceAspect(2)]
    [SecuredOperation("author.add,admin,editor,user")]
    [ValidationAspect(typeof(AuthorValidator))]
    [TransactionScopeAspect]
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
    [CacheRemoveAspect("IAuthorService.Get")]
    [PerformanceAspect(2)]
    [TransactionScopeAspect]
    public IResult Update(Author author)
    {
        _authorDal.Update(author);
        return new SuccessResult(Messages.AuthorUpdated);
    }

    [SecuredOperation("author.update,admin,editor")]
    [CacheRemoveAspect("IAuthorService.Get")]
    [PerformanceAspect(2)]
    [TransactionScopeAspect]
    public IResult Delete(Guid authorId, bool permanently = false)
    {
        Author author = _authorDal.Get(author => author.AuthorId == authorId);
        if (!permanently)
        {
            author.IsDeleted = true;
            author.DeleteTime = DateTime.UtcNow;
            _authorDal.Update(author);
            return new SuccessResult(Messages.AuthorDeleted);
        }
        else
        {
            _authorDal.Delete(author);
            return new SuccessResult(Messages.AuthorDeletedPermanently);
        }

        return new ErrorResult(Messages.AuthorNotDeleted);
    }
}