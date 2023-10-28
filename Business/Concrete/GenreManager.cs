using Business.Abstract;
using Business.Constants;
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

    public IDataResult<List<Genre>> GetAll()
    {
        List<Genre> genres = _genreDal.GetAll();
        if (genres != null)
        {
            return new SuccessDataResult<List<Genre>>(genres, Messages.GenresListed);
        }

        return new ErrorDataResult<List<Genre>>(genres, Messages.GenresNotListed);
    }

    public IDataResult<Genre> GetById(Guid genreId)
    {
        Genre genre = _genreDal.Get(g => g.GenreId == genreId);
        if (genre == null)
        {
            return new ErrorDataResult<Genre>(genre, Messages.GenreNotFound);
        }

        return new SuccessDataResult<Genre>(genre, Messages.GenreWasFound);
    }

    public IDataResult<Genre> GetByName(string genreName)
    {
        Genre genre = _genreDal.Get(genre => genre.GenreName.ToLower() == genreName.ToLower());
        if (genre != null)
        {
            return new SuccessDataResult<Genre>(genre, Messages.GenreWasFound);
        }

        return new ErrorDataResult<Genre>(genre, Messages.GenreNotFound);
    }

    public IResult Add(Genre genre)
    {
        if (genre.GenreId == null)
        {
            genre.GenreId = Guid.NewGuid();
        }

        _genreDal.Add(genre);
        return new SuccessResult(Messages.GenreAdded);
    }
}