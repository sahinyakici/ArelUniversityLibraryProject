﻿using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract;

public interface IBookService
{
    IDataResult<List<Book>> GetAll();
    IResult Add(BookDTO bookDto);
    IResult Update(Book book);
    IResult Delete(Book book);
    IDataResult<List<Book>> GetAllByGenre(Guid genreId);
    IDataResult<List<Book>> GetAllByAuthor(Guid authorId);
    IDataResult<List<Book>> GetAllByOwnerName(string ownerName);
    IDataResult<Book> GetById(Guid id);
}