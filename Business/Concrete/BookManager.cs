using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete;

public class BookManager : IBookService
{
    private readonly IMapper _mapper;
    private IAuthorService _authorService;
    private IBookDal _bookDal;
    private IGenreService _genreService;
    private IUserService _userService;

    public BookManager(IBookDal bookDal, IAuthorService authorService, IGenreService genreService, IMapper mapper,
        IUserService userService)
    {
        _bookDal = bookDal;
        _authorService = authorService;
        _genreService = genreService;
        _mapper = mapper;
        _userService = userService;
    }

    public IDataResult<List<Book>> GetAll()
    {
        List<Book> books = _bookDal.GetAll();
        if (books != null)
        {
            return new SuccessDataResult<List<Book>>(books, Messages.BooksListed);
        }

        return new ErrorDataResult<List<Book>>(Messages.BooksNotListed);
    }

    [ValidationAspect(typeof(BookDtoValidator))]
    [SecuredOperation("books.add,admin,editor,user")]
    public IResult Add(BookDTO bookDto)
    {
        BusinessRules.Run(CreateGenreIfNotExists(bookDto.GenreName), CreateAuthorIfNotExists(bookDto.AuthorName));
        Book book = _mapper.Map<Book>(bookDto);
        _bookDal.Add(book);
        return new SuccessResult(Messages.BookAdded);
    }

    [ValidationAspect(typeof(BookDtoValidator))]
    [SecuredOperation("books.update,admin,editor,user")]
    public IResult Update(Book book)
    {
        _bookDal.Update(book);
        return new SuccessResult(Messages.BookUpdated);
    }

    [SecuredOperation("books.delete,admin,editor,user")]
    public IResult Delete(Book book)
    {
        _bookDal.Delete(book);
        return new SuccessResult(Messages.BookDeleted);
    }

    public IDataResult<List<Book>> GetAllByGenre(Guid genreId)
    {
        List<Book> books = _bookDal.GetAll(b => b.GenreId == genreId);
        if (books != null)
        {
            return new SuccessDataResult<List<Book>>(books, Messages.BooksListed);
        }

        return new ErrorDataResult<List<Book>>(Messages.BooksNotListed);
    }

    public IDataResult<List<Book>> GetAllByAuthor(Guid authorId)
    {
        List<Book> books = _bookDal.GetAll(b => b.AuthorId == authorId);
        if (books != null)
        {
            return new SuccessDataResult<List<Book>>(books, Messages.BooksListed);
        }

        return new ErrorDataResult<List<Book>>(Messages.BooksNotListed);
    }

    public IDataResult<List<Book>> GetAllByOwnerName(string ownerName)
    {
        var result = _userService.GetByFirstName(ownerName);
        if (result.Success)
        {
            User user = result.Data;
            List<Book> books = _bookDal.GetAll(book => book.OwnerId == user.UserId);
            return new SuccessDataResult<List<Book>>(books, Messages.BooksListed);
        }

        return new ErrorDataResult<List<Book>>(Messages.UserNotFound);
    }

    public IDataResult<Book> GetById(Guid id)
    {
        var result = _bookDal.Get(book => book.BookId == id);
        if (result != null)
        {
            return new SuccessDataResult<Book>(_bookDal.Get(book => book.BookId == id), Messages.BooksListed);
        }

        return new ErrorDataResult<Book>(Messages.BookNotFound);
    }

    private IDataResult<Genre> CreateGenreIfNotExists(string genreName)
    {
        var result = _genreService.GetByName(genreName);
        if (result.Success)
        {
            return new SuccessDataResult<Genre>(result.Data, Messages.GenreWasFound);
        }

        Genre genre = new Genre { GenreName = genreName, GenreId = Guid.NewGuid() };
        var createResult = _genreService.Add(genre);
        if (createResult.Success)
        {
            return new SuccessDataResult<Genre>(genre, Messages.GenreCreated);
        }

        return new ErrorDataResult<Genre>(Messages.GenreNotFound);
    }

    private IDataResult<Author> CreateAuthorIfNotExists(string authorName)
    {
        var result = _authorService.GetByName(authorName);
        if (result.Success)
        {
            return new SuccessDataResult<Author>(result.Data, Messages.AuthorWasFound);
        }

        Author author = new Author { AuthorName = authorName, AuthorId = Guid.NewGuid() };
        var createResult = _authorService.Add(author);
        if (createResult.Success)
        {
            return new SuccessDataResult<Author>(author, createResult.Message);
        }

        return new ErrorDataResult<Author>(Messages.AuthorNotFound);
    }
}