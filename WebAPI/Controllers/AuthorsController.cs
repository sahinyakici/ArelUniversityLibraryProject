using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IAuthorService _authorManager;

        public AuthorsController(IAuthorService authorManager)
        {
            _authorManager = authorManager;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _authorManager.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(Guid guid)
        {
            var result = _authorManager.GetById(guid);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(Author author)
        {
            var result = _authorManager.Add(author);
            if (result.Success)
            {
                return Ok(result.Success);
            }

            return BadRequest(result.Message);
        }
    }
}