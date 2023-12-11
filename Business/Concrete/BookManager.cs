using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.Extensions.Configuration;

namespace Business.Concrete;

public class BookManager : IBookService
{
    private readonly IMapper _mapper;
    private IAuthorService _authorService;
    private IBookDal _bookDal;
    private IGenreService _genreService;
    private IUserService _userService;
    private IImageService _imageService;

    public BookManager(IBookDal bookDal, IAuthorService authorService, IGenreService genreService, IMapper mapper,
        IUserService userService, IImageService imageService)
    {
        _bookDal = bookDal;
        _authorService = authorService;
        _genreService = genreService;
        _mapper = mapper;
        _userService = userService;
        _imageService = imageService;
    }

    [CacheAspect]
    [PerformanceAspect(10)]
    public IDataResult<List<Book>> GetAll(bool withDeleted = false)
    {
        List<Book> books = _bookDal.GetAll(book => withDeleted || !book.IsDeleted);
        if (books != null)
        {
            return new SuccessDataResult<List<Book>>(books, Messages.BooksListed);
        }

        return new ErrorDataResult<List<Book>>(Messages.BooksNotListed);
    }

    [CacheAspect]
    [PerformanceAspect(10)]
    public IDataResult<List<Book>> GetAllNotRented(bool withDeleted = false)
    {
        List<Book> books = _bookDal.GetAll(book => book.RentStatus == false && (withDeleted || !book.IsDeleted));
        return new SuccessDataResult<List<Book>>(books);
    }

    [ValidationAspect(typeof(BookDtoValidator))]
    [SecuredOperation("books.add,admin,editor,user")]
    [CacheRemoveAspect("IBookService.Get")]
    [TransactionScopeAspect]
    [PerformanceAspect(2)]
    public IResult Add(BookDTO bookDto)
    {
        bookDto.BookId = Guid.NewGuid();
        IResult results = BusinessRules.Run(CreateGenreIfNotExists(bookDto.GenreName),
            CreateAuthorIfNotExists(bookDto.AuthorName),
            SaveImage(bookDto.BookId, bookDto.ImagePath));
        if (results != null)
        {
            return new ErrorResult(results.Message);
        }
        else
        {
            Book book = _mapper.Map<Book>(bookDto);
            _bookDal.Add(book);
            return new SuccessResult(Messages.BookAdded);
        }
    }

    [ValidationAspect(typeof(BookDtoValidator))]
    [SecuredOperation("books.update,admin,editor,user")]
    [CacheRemoveAspect("IBookService.Get")]
    [TransactionScopeAspect]
    [PerformanceAspect(2)]
    public IResult Update(Book book)
    {
        _bookDal.Update(book);
        return new SuccessResult(Messages.BookUpdated);
    }

    [SecuredOperation("books.delete,admin,editor,user")]
    [CacheRemoveAspect("IBookService.Get")]
    [TransactionScopeAspect]
    [PerformanceAspect(2)]
    public IResult Delete(Guid id, bool permanently = false)
    {
        Book deleteBook = _bookDal.Get(b => b.BookId == id);
        if (!permanently)
        {
            deleteBook.IsDeleted = true;
            deleteBook.DeleteTime = DateTime.UtcNow;
            _bookDal.Update(deleteBook);
        }
        else
        {
            _bookDal.Delete(deleteBook);
        }

        return new SuccessResult(Messages.BookDeleted);
    }

    [CacheAspect]
    [PerformanceAspect(5)]
    public IDataResult<List<Book>> GetAllByGenre(Guid genreId, bool withDelete = false)
    {
        List<Book> books = _bookDal.GetAll(b => b.GenreId == genreId && (withDelete || !b.IsDeleted));
        if (books != null)
        {
            return new SuccessDataResult<List<Book>>(books, Messages.BooksListed);
        }

        return new ErrorDataResult<List<Book>>(Messages.BooksNotListed);
    }

    [CacheAspect]
    [PerformanceAspect(5)]
    public IDataResult<List<Book>> GetAllByAuthorName(string authorName, bool withDelete = false)
    {
        Guid authorId = _authorService.GetByName(authorName).Data.AuthorId;
        List<Book> books = _bookDal.GetAll(b => b.AuthorId == authorId && (withDelete || !b.IsDeleted));
        if (books != null)
        {
            return new SuccessDataResult<List<Book>>(books, Messages.BooksListed);
        }

        return new ErrorDataResult<List<Book>>(Messages.BooksNotListed);
    }

    [CacheAspect]
    [PerformanceAspect(5)]
    public IDataResult<List<Book>> GetAllByOwnerName(string ownerName, bool withDelete = false)
    {
        var result = _userService.GetByUserName(ownerName);
        if (result.Success)
        {
            User user = result.Data;
            List<Book> books = _bookDal.GetAll(book =>
                book.OwnerId == user.UserId && (withDelete || !book.IsDeleted));
            return new SuccessDataResult<List<Book>>(books, Messages.BooksListed);
        }

        return new ErrorDataResult<List<Book>>(Messages.UserNotFound);
    }

    [CacheAspect]
    [PerformanceAspect(5)]
    public IDataResult<Book> GetById(Guid id, bool withDelete = false)
    {
        var result = _bookDal.Get(book => book.BookId == id && (withDelete || !book.IsDeleted));
        if (result != null)
        {
            return new SuccessDataResult<Book>(result, Messages.BooksListed);
        }

        return new ErrorDataResult<Book>(Messages.BookNotFound);
    }

    public IResult RentalABook(Guid bookId)
    {
        Book updateBook = _bookDal.Get(book => book.BookId == bookId);
        updateBook.RentStatus = true;
        _bookDal.Update(updateBook);
        return new SuccessResult(Messages.BookUpdated);
    }

    public IResult CancelRentalABook(Guid bookId)
    {
        Book updateBook = _bookDal.Get(book => book.BookId == bookId);
        updateBook.RentStatus = false;
        _bookDal.Update(updateBook);
        return new SuccessResult(Messages.BookUpdated);
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

    private IResult SaveImage(Guid bookId, String imagePath)
    {
        if (imagePath == null)
        {
            return new SuccessResult();
        }

        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        try
        {
            if (File.Exists(imagePath))
            {
                string newFileName = $"{Guid.NewGuid()}{Path.GetExtension(imagePath)}";
                string imageDirectory = configuration["ImageFolderPath"];
                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                string destinationPath = Path.Combine(imageDirectory, newFileName);
                File.Copy(imagePath, destinationPath);
                Console.WriteLine($"The image has been successfully copied and renamed: {newFileName}");
                Image image = new Image
                {
                    ImagePath = "assets/images/" + newFileName, IsDeleted = false, ImageId = Guid.NewGuid(),
                    BookId = bookId
                };
                _imageService.Add(image);
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult(Messages.ErroFileCopy);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
            return new ErrorResult(ex.Message);
        }
    }
}