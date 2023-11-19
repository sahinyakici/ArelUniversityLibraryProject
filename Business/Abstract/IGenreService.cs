using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract;

public interface IGenreService
{
    IDataResult<List<Genre>> GetAll(bool withDeleted = false);
    IDataResult<Genre> GetById(Guid genreId, bool withDeleted = false);
    IDataResult<Genre> GetByName(string genreName, bool withDeleted = false);
    IResult Add(Genre genre);
    IResult Update(Genre genre);
    IResult Delete(Guid genreId, bool permanently = false);
}