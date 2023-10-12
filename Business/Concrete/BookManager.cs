using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete;

public class BookManager : IBookService
{
    private IBookDal _bookDal;

    public BookManager(IBookDal bookDal)
    {
        _bookDal = bookDal;
    }

    public IDataResult<List<Book>> GetAll()
    {
        return new SuccessDataResult<List<Book>>(_bookDal.GetAll(), Messages.BooksListed);
    }

    [ValidationAspect(typeof(BookValidator))]
    public IResult Add(Book book)
    {
        book.BookId = Guid.NewGuid();
        _bookDal.Add(book);
        return new SuccessResult(Messages.BookAdded);
    }

    public IResult Update(Book book)
    {
        _bookDal.Update(book);
        return new SuccessResult(Messages.BookUpdated);
    }

    public IResult Delete(Book book)
    {
        _bookDal.Delete(book);
        return new SuccessResult(Messages.BookDeleted);
    }

    public IDataResult<List<Book>> GetAllByGenre(Guid genreId)
    {
        return new SuccessDataResult<List<Book>>(_bookDal.GetAll(b => b.GenreId == genreId), Messages.BooksListed);
    }

    public IDataResult<List<Book>> GetAllByAuthor(Guid authorId)
    {
        return new SuccessDataResult<List<Book>>(_bookDal.GetAll(b => b.AuthorId == authorId), Messages.BooksListed);
    }

    public IDataResult<List<BookDetailDto>> GetBookDetails()
    {
        return new SuccessDataResult<List<BookDetailDto>>(_bookDal.GetBookDetails(), Messages.BooksListed);
    }

    public IDataResult<Book> GetById(Guid id)
    {
        return new SuccessDataResult<Book>(_bookDal.Get(book => book.BookId == id), Messages.BooksListed);
    }
}