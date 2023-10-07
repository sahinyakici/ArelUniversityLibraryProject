using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract;

public interface IBookService
{
    IDataResult<List<Book>> GetAll();
    IResult Add(Book book);
    IResult Update(Book book);
    IResult Delete(Book book);

    IDataResult<List<Book>> GetAllByGenre(Guid genreId);
    IDataResult<List<Book>> GetAllByAuthor(Guid authorId);
    IDataResult<List<BookDetailDto>> GetBookDetails();
    IDataResult<Book> GetById(Guid id);
}