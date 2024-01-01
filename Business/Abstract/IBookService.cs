using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract;

public interface IBookService
{
    IDataResult<List<Book>> GetAll(bool withDelete = false);
    IDataResult<List<Book>> GetAllNotRented(bool withDeleted = false);
    IResult Add(BookDTO bookDto, IFormFile? image);
    IResult Update(BookDTO book);
    IResult Delete(Guid id, bool permanently = false);
    IDataResult<List<Book>> GetAllByGenre(Guid genreId, bool withDelete = false);
    IDataResult<List<Book>> GetAllByAuthorName(string authorName, bool withDelete = false);
    IDataResult<List<Book>> GetAllByOwnerName(string ownerName, bool withDelete = false);
    IDataResult<Book> GetById(Guid id, bool withDelete = false);
    IResult RentalABook(Guid bookId);
    IResult CancelRentalABook(Guid bookId);
}