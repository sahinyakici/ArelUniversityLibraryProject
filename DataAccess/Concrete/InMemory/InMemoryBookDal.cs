using System.Linq.Expressions;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.InMemory;

public class InMemoryBookDal : IBookDal
{
    private List<Book> _books;

    public InMemoryBookDal()
    {
        _books = new List<Book>
        {
            new Book
            {
                BookId = new Guid("71a903de-39ad-4ba9-8ee6-26b588907fb0"),
                AuthorId = new Guid("f9d77ec4-cada-46fc-b4b3-69e530fcd60d"), BookName = "Nutuk", Location = "A324",
                GenreId = new Guid("baa7bfb1-bd9c-441c-a1f2-5e029c6ef3e6"), PageSize = 543
            },
            new Book
            {
                BookId = new Guid("a807b8c7-393a-4ec7-81c6-0cd28ff72f0f"),
                AuthorId = new Guid("9f3ac55f-6fd0-4891-b1e2-a284f105823e"), BookName = "Ucurtma Avcısı",
                Location = "A384",
                GenreId = new Guid("9642fb4b-3db4-438b-9ec3-dabde39e3117"), PageSize = 375
            }
        };
    }

    public Book Get(Expression<Func<Book, bool>> filter = null)
    {
        throw new NotImplementedException();
    }

    public List<Book> GetAll(Expression<Func<Book, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public void Add(Book book)
    {
        _books.Add(book);
    }

    public void Update(Book book)
    {
        Book bookToUpdate = _books.SingleOrDefault(b => b.BookId == book.BookId);
        bookToUpdate.BookName = book.BookName;
        bookToUpdate.Location = book.Location;
        bookToUpdate.AuthorId = book.AuthorId;
        bookToUpdate.PageSize = book.PageSize;
        bookToUpdate.GenreId = book.GenreId;
    }

    public void Delete(Book book)
    {
        Book bookToDelete = _books.SingleOrDefault(b => b.BookId == book.BookId);
        _books.Remove(bookToDelete);
    }

    public List<BookDTO> GetBookDetails()
    {
        throw new NotImplementedException();
    }

    public List<Book> GetAll()
    {
        return _books;
    }

    public List<Book> GetAllByGenre(Guid genreId)
    {
        return _books.Where(b => b.GenreId == genreId).ToList();
    }

    public List<Book> GetAllByAuthor(Guid authorId)
    {
        return _books.Where(b => b.AuthorId == authorId).ToList();
    }
}