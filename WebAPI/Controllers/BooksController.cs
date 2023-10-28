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
        public IActionResult GetAll()
        {
            var result = _bookService.GetAll();
            if (result.Success)
            {
                List<Book> books = result.Data;
                List<BookDTO> resultBookDetailDto = books.Select(book => _mapper.Map<BookDTO>(book)).ToList();
                return Ok(resultBookDetailDto);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(Guid guid)
        {
            var result = _bookService.GetById(guid);
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
        public IActionResult GetAllByOwnerName(string ownerName)
        {
            var result = _bookService.GetAllByOwnerName(ownerName);
            if (result.Success)
            {
                List<Book> books = result.Data;
                List<BookDTO> bookDtos = books.Select(book => _mapper.Map<BookDTO>(book)).ToList();
                return Ok(bookDtos);
            }

            return BadRequest(result.Message);
        }
    }
}