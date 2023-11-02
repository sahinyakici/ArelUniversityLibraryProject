using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract;

public interface IGenreService
{
    IDataResult<List<Genre>> GetAll();
    IDataResult<Genre> GetById(Guid genreId);
    IDataResult<Genre> GetByName(string genreName);
    IResult Add(Genre genre);
    IResult Update(Genre genre);
}