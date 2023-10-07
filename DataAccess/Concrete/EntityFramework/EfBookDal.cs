using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework;

public class EfBookDal : EfEntityRepositoryBase<Book, PostgreContext>, IBookDal
{
    public List<BookDetailDto> GetBookDetails()
    {
        using (PostgreContext context = new PostgreContext())
        {
            var result = from book in context.Books
                join genre in context.Genres on book.GenreId equals genre.GenreId
                join author in context.Authors on book.AuthorId equals author.AuthorId
                select new BookDetailDto
                {
                    BookId = book.BookId, GenreName = genre.GenreName, BookName = book.BookName,
                    PageSize = book.PageSize, AuthorName = author.AuthorName, Location = book.Location
                };
            return result.ToList();
        }
    }
}