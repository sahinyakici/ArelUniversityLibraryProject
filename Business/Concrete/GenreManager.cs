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

public class GenreManager : IGenreService
{
    private IGenreDal _genreDal;

    public GenreManager(IGenreDal genreDal)
    {
        _genreDal = genreDal;
    }

    [PerformanceAspect(2)]
    [CacheAspect]
    public IDataResult<List<Genre>> GetAll()
    {
        List<Genre> genres = _genreDal.GetAll();
        if (genres != null)
        {
            return new SuccessDataResult<List<Genre>>(genres, Messages.GenresListed);
        }

        return new ErrorDataResult<List<Genre>>(Messages.GenresNotListed);
    }

    [PerformanceAspect(2)]
    [CacheAspect]
    public IDataResult<Genre> GetById(Guid genreId)
    {
        Genre genre = _genreDal.Get(g => g.GenreId == genreId);
        if (genre == null)
        {
            return new ErrorDataResult<Genre>(Messages.GenreNotFound);
        }

        return new SuccessDataResult<Genre>(genre, Messages.GenreWasFound);
    }

    [PerformanceAspect(2)]
    [CacheAspect]
    public IDataResult<Genre> GetByName(string genreName)
    {
        Genre genre = _genreDal.Get(genre => genre.GenreName.ToLower() == genreName.ToLower());
        if (genre != null)
        {
            return new SuccessDataResult<Genre>(genre, Messages.GenreWasFound);
        }

        return new ErrorDataResult<Genre>(Messages.GenreNotFound);
    }

    [CacheRemoveAspect("IGenreService.Get")]
    [SecuredOperation("genre.add,admin,editor,user")]
    [ValidationAspect(typeof(GenreValidator))]
    [TransactionScopeAspect]
    public IResult Add(Genre genre)
    {
        if (genre.GenreId == null)
        {
            genre.GenreId = Guid.NewGuid();
        }

        _genreDal.Add(genre);
        return new SuccessResult(Messages.GenreAdded);
    }

    [CacheRemoveAspect("IGenreService.Get")]
    [SecuredOperation("genre.add,admin,editor")]
    [ValidationAspect(typeof(GenreValidator))]
    [TransactionScopeAspect]
    public IResult Update(Genre genre)
    {
        if (genre.GenreId == null)
        {
            return new ErrorResult(Messages.IdIsRequired);
        }

        _genreDal.Update(genre);
        return new SuccessResult(Messages.GenreWasUpdated);
    }
}