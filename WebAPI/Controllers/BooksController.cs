using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        IBookService _bookService;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll(bool withDeleted)
        {
            var result = _bookService.GetAll(withDeleted);
            if (result.Success)
            {
                List<Book> books = result.Data;
                List<BookDTO> resultBookDetailDto = books.Select(book => _mapper.Map<BookDTO>(book)).ToList();
                return Ok(resultBookDetailDto);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(Guid guid, bool withDeleted)
        {
            var result = _bookService.GetById(guid, withDeleted);
            if (result.Success)
            {
                Book book = result.Data;
                BookDTO bookDto = _mapper.Map<BookDTO>(book);
                return Ok(bookDto);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("Add")]
        public IActionResult Add(BookDTO bookDto)
        {
            var result = _bookService.Add(bookDto);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetAllByOwnerName")]
        public IActionResult GetAllByOwnerName(string ownerName, bool withDeleted)
        {
            var result = _bookService.GetAllByOwnerName(ownerName, withDeleted);
            if (result.Success)
            {
                List<Book> books = result.Data;
                List<BookDTO> bookDtos = books.Select(book => _mapper.Map<BookDTO>(book)).ToList();
                return Ok(bookDtos);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetAllByAuthorName")]
        public IActionResult GetAllByAuthorName(string authorName, bool withDeleted)
        {
            var result = _bookService.GetAllByAuthorName(authorName, withDeleted);
            if (result.Success)
            {
                List<Book> books = result.Data;
                List<BookDTO> bookDtos = books.Select(book => _mapper.Map<BookDTO>(book)).ToList();
                return Ok(bookDtos);
            }

            return BadRequest(result.Message);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(Guid id, bool permanently)
        {
            var result = _bookService.Delete(id, permanently);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}