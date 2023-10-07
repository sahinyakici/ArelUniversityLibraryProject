using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract;

public interface IGenreService
{
    IDataResult<List<Genre>> GetAll();
    IDataResult<Genre> GetById(Guid genreId);
    IResult Add(Genre genre);
}