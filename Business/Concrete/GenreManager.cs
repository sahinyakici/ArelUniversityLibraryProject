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
        return new SuccessDataResult<List<Genre>>(_genreDal.GetAll());
    }

    public IDataResult<Genre> GetById(Guid genreId)
    {
        return new SuccessDataResult<Genre>(_genreDal.Get(g => g.GenreId == genreId));
    }

    public IResult Add(Genre genre)
    {
        genre.GenreId = Guid.NewGuid();
        _genreDal.Add(genre);
        return new SuccessResult(Messages.GenreAdded);
    }
}